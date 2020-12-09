#Imports
import flask
from flask import request
import cv2
import mxnet as mx
import pandas as pd
import numpy as np
import time
import pymssql
import re
import os
from geopy.distance import geodesic
from sklearn.preprocessing import normalize
from tqdm import tqdm
import torch
from datetime import datetime,timedelta
import sys
import warnings
warnings.filterwarnings("ignore")
import glob
sys.path.append("./FaceDetector/")
from FaceDetector.detect_functions import inference,preprocess

#Embedding Model building
def get_model(ctx, image_size, model_str, layer):
    prefix = "./FaceRecognition/model-r100-ii/model"
    epoch = 0
    print('loading',prefix, epoch)
    sym, arg_params, aux_params = mx.model.load_checkpoint(prefix, epoch)
    all_layers = sym.get_internals()
    sym = all_layers[layer+'_output']
    model = mx.mod.Module(symbol=sym, context=ctx, label_names = None)
    model.bind(data_shapes=[('data', (1, 3, image_size[0], image_size[1]))])
    model.set_params(arg_params, aux_params)
    return model

#Configs
sim_thresh=0.30
det_thresh=0.85
km_thresh=5 #search cameras 5KM radius
#Initiliaze
x=torch.rand((800,800,3))
inference(x)
if torch.cuda.is_available():
    embedding_model = get_model(mx.gpu(), (112, 112), None, 'fc1')
else:
    embedding_model = get_model(mx.cpu(), (112, 112), None, 'fc1')


#Find if person is found using detections
def findmatch(personE,dets,imgO):
    imgc=imgO.copy()
    found=False
    for b in dets:
        if b[4] < det_thresh:
            continue
        r1=max(0,int(b[1]))
        r2=min(imgO.shape[0],int(b[3]))
        c1=max(0,int(b[0]))
        c2=min(imgO.shape[1],int(b[2]))
        cropimg=imgO[r1:r2,c1:c2]
        if cropimg.shape[0]==0 or cropimg.shape[1]==0:
            continue

        #Recogniton
        #Align it
        align_image=preprocess(imgO,b[0:4],b[5:].reshape(5,2),image_size='112,112')
        person_detectedE=get_feature(align_image)
        sim=abs(np.dot(personE,person_detectedE))
        if sim>sim_thresh:
            found=True
            cv2.rectangle(imgc, (b[0], b[1]), (b[2], b[3]), (255, 0, 0), 2)
    if found:
        return imgc
            
#todatetime
def str2datetime(s):
    c=np.array(re.split('[- : _]',s)).astype('int')
    c=datetime(c[2],c[1],c[0],c[3],c[4])
    return c

#Get the camera feed on specific interval
def getcameraI(cameraF,time_period):
    lower=str2datetime(time_period[0])
    higher=str2datetime(time_period[1])
    cameraAll=glob.glob(cameraF+'/*')
    cameraSI=[]
    for cpath in cameraAll:
        c=((cpath.split("/")[-1]).split(".")[0]).split(" ")
        c_lower,c_higher=c[0],c[1]
        c_lower=str2datetime(c_lower)
        c_higher=str2datetime(c_higher)
        
        if lower>=c_lower and lower<=c_higher:
            cameraSI.append(cpath)
        elif higher>=c_lower and higher<=c_higher:
            cameraSI.append(cpath)
        elif c_lower>=lower and c_higher<=higher:
            cameraSI.append(cpath)
    return cameraSI

#Searches the camera feed on certrain intervals
def searchcameraF(case_id,personE,cameraF,time_period):
    cameraI=getcameraI(cameraF,time_period)
    print(cameraF)
    print("Camera Selected: ",cameraF.split("/")[-1]," ",cameraI)
    
    #For each video clip selected from one camera
    cameraid=str(cameraF.split("/")[-1])
    index=0
    for camera in cameraI:
        cap = cv2.VideoCapture(camera)
        total_frames= int(cap.get(cv2.CAP_PROP_FRAME_COUNT))
        fps=cap.get(cv2.CAP_PROP_FPS)
        success, frame = cap.read()
        findex=0
        loop=tqdm(total=total_frames)
        while(success):
            frame=frame[:,:,::-1]
            dets=inference(frame)
            img_output=findmatch(personE,dets,frame.copy())
            if img_output is not None:
                imgname="{}_{}.jpg".format(cameraid,str(index))
                cv2.imwrite("../../dist/\SafeTechContent/Processed/{}/{}".format(case_id,imgname),img_output[:,:,::-1])
                if findex<fps:
                    delta=0
                    time_found=str2datetime(time_period[0])
                else:
                    delta=timedelta(seconds=findex//fps)
                    time_found=str2datetime(time_period[0])+delta
                #Save to DB
                sql = "INSERT INTO Processed VALUES ({}, '{}', '{}', {});".format(case_id,imgname,str(time_found),cameraid)
                cursor.execute(sql)

                index+=1
            loop.update(1)
            success, frame = cap.read()
            findex+=1

#GetEmbedding of the face
def get_feature(img_aligned):
    aligned = np.transpose(img_aligned, (2,0,1))
    aligned=aligned[np.newaxis,...]
    data = mx.nd.array(aligned)
    db = mx.io.DataBatch(data=(data,))
    embedding_model.forward(db, is_train=False)
    embedding = embedding_model.get_outputs()[0].asnumpy()
    embedding = normalize(embedding).flatten()
    return embedding

#Returns cameras which needed to be searched for the person
def sortedCameras(lat,long,time_period):
    d=[]
    for _,row in df_cameras.iterrows():
        dis=geodesic((lat,long),(row['lat'],row['long'])).kilometers
        d.append(dis)
    tempdf=df_cameras.copy()
    tempdf['dist']=d
    print(tempdf)
    tempdf=tempdf.loc[tempdf['dist']<=km_thresh,:]
    tempdf.sort_values(by='dist',ascending=False,inplace=True)
    return tempdf['id'].values

#Get Embedding of person to search
def getpersonE(image_path):
    img=cv2.imread(image_path)[:,:,::-1]
    img=cv2.resize(img,(480,600))
    imgc=img.copy()
    dets=inference(img)
    #print(dets)
    b=dets[dets[:,4].argmax(),:]
    cropped_align_face=preprocess(imgc,b[0:4],b[5:].reshape(5,2),image_size='112,112')
    cv2.imwrite('testx.jpg',img)
    cv2.imwrite('test.jpg',cropped_align_face)
    cv2.imwrite('test_simple.jpg',imgc[int(b[1]):int(b[3]),int(b[0]):int(b[2])])
    em=get_feature(cropped_align_face)
    return em

#Smartly Search for the person in closest cameras
def smartSearch(case_id,lat,long,time_period):
    print("Searching")
    os.makedirs('../../dist/\SafeTechContent/Processed/{}'.format(str(case_id)),exist_ok=True)
    person=glob.glob('../../dist/\SafeTechContent/Requests/{}/*'.format(str(case_id)))
    if len(person)==0:
        return "No Image Found"
    person=person[0]
    camerasId=sortedCameras(lat,long,time_period)
    cameras2search=df_cameras.loc[df_cameras['id'].isin(camerasId),:]
    print("To Search")
    print(cameras2search)
    #Get Embedding of person to search
    personE=getpersonE(person)
    for _,row in cameras2search.iterrows():
        cameraF='../CameraFeed/'+str(row['city'])+'/'+str(row['id'])
        searchcameraF(case_id,personE,cameraF,time_period)
    con.commit()
    return "Completed"

#Connection with DB
host = 'safetech.ce2wpfxccpai.us-east-2.rds.amazonaws.com'
user = 'admin'
password = 'ammad777$'
database = 'ForeSpark'
port=1433
con=pymssql.connect(host,user,password,database)
cursor = con.cursor()

#Get Cameras
df_cameras=[]
sql = "Select Id,address, CityId, lat, lng from Installations where Status=1"
cursor.execute(sql)
results = cursor.fetchall()
for row in results:
    df_cameras.append(row)
df_cameras=pd.DataFrame(df_cameras,columns=['id','location','city','lat','long'])
print(df_cameras)

#Flask API
app = flask.Flask(__name__)
app.config["DEBUG"] = False
app.config['PROPAGATE_EXCEPTIONS'] = True

@app.errorhandler(500)
def internal_error(exception):
    print ("500 error caught")
    print (traceback.format_exc())
    
@app.route('/api/')
@app.route('/api')
def api_all():
    case = request.args.get('case', type=int)
    lat = float(request.args.get('lat', type=str))
    lon = float(request.args.get('lon', type=str))
    start = request.args.get('start', type=str)
    end = request.args.get('end', type=str)

    print(case,lat,lon,start,end)
    #HIT
    status=smartSearch(case,lat,lon,[start,end])
    return status

#app.run(port=5000)
app.run(host='0.0.0.0')