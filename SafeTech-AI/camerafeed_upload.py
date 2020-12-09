import cv2
from datetime import datetime

t = datetime.now()
filename = './CameraFeed/Islamabad/DhaPhase2/1/{}.avi'
vc = cv2.VideoCapture(0)

#VideoOutput
img_size=(640,480)
fps=30.0
fourcc = cv2.VideoWriter_fourcc('M','J','P','G')
output = cv2.VideoWriter(filename.format(t.strftime("%d-%m-%Y_%H:%M")), fourcc, fps, img_size)

#Read Frames
success, frame = vc.read()
while success:
    #Check if 10 mins passes
    now = datetime.now()
    if (now-t).seconds>60*10:
        t=now
        output.release()
        output = cv2.VideoWriter(filename.format(now.strftime("%d-%m-%Y_%H:%M")), fourcc, fps, img_size)
    output.write(frame)      
    success, frame = vc.read()