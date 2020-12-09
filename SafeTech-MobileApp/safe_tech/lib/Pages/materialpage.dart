import 'dart:ffi';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';

import 'package:safe_tech/Pages/dashboardpage.dart';
import 'package:safe_tech/utils/colors.dart';
import 'package:date_field/date_field.dart';
import 'package:http/http.dart' as http;
import 'package:dio/dio.dart';

import 'dart:io';
import 'package:flutter/services.dart' show rootBundle;
import 'package:path_provider/path_provider.dart' as filePath;
import 'package:image_picker/image_picker.dart';
import 'package:safe_tech/Pages/requestsubmitpage.dart';
import 'dart:io';
import 'dart:async';
import 'dart:convert';

String _name = "Muhammad Ali";
String _cnic = "3740523488597";
String _city = "Islamabad";
String _address = "Street 24 G-10/2";
String _description = "My son is missing for last 2 hours";
double _lat = 33.6740158896247;
double _lng = 73.01293583955079;
int _cityId = 5;
String _startTime = "2020-11-20 T18:50";
String _endTime = "2020-11-20 T19:12";
String _location = "33.674015.. , 73.012935..";
String _image = "missingPerson.jpeg";

class ImageUtils {
  static Future<File> imageToFile({String imageName}) async {
    var bytes = await rootBundle.load('$imageName');
    String tempPath = (await filePath.getTemporaryDirectory()).path;
    File file = File('$tempPath/img.png');
    await file.writeAsBytes(
        bytes.buffer.asUint8List(bytes.offsetInBytes, bytes.lengthInBytes));
    return file;
  }
}

class MaterialSamplePage extends StatefulWidget {
  // var cameras;
  //MaterialSamplePage(this.cameras);
  @override
  _MaterialSamplePageState createState() => _MaterialSamplePageState();
}

Future createCustomFeedRequest() async {
  Response response;
  BaseOptions options = new BaseOptions(
      baseUrl: "http://3.140.162.29:21022/api/Request",
      connectTimeout: 6000,
      receiveTimeout: 3000,
      headers: {
        'Content-type': 'application/json; charset=UTF-8',
      });
  Dio dio = new Dio(options);

  String toJsonString =
      "\{ \"startTime\":\"$_startTime \",\"endTime\":\"$_endTime \",\"cnic\" : \" $_cnic \" ,\"name\": \"$_name\", \"address\": \"$_address\",\"description\": \"$_description\", \"lat\": $_lat,\"lng\": $_lng,\"cityId\": $_cityId}";

  print("Final statement " + toJsonString);
  File file =
      await ImageUtils.imageToFile(imageName: 'assets/images/culprit.jpeg');

  FormData formData = new FormData.fromMap({
    "requestForm": toJsonString,
    "image_1": await MultipartFile.fromFile(file.path, filename: 'culprit.jpeg')
  });

  final String uri = "http://3.140.162.29:21022/api/Request/CreateNewRequest";

  response = await dio.post("/CreateNewRequest", data: formData);
  return response.data;
  /* FormData formData = new FormData.fromMap({"requestForm": toJsonString});

  final String uri = "http://3.140.162.29:21022/api/Request/CreateNewRequest";

  response = await dio.post("/CreateNewRequest", data: formData);
  return response.data; */
}

class City {
  const City(this.cname, this.cid);
  final String cname;
  final int cid;
}

class _MaterialSamplePageState extends State<MaterialSamplePage> {
  var name = TextEditingController(text: _name);
  var cnic = TextEditingController(text: _cnic);
  var location = TextEditingController(text: _location);
  var description = TextEditingController(text: _description);
  var city = TextEditingController(text: "Islamabad");
  var lat = TextEditingController();
  var lng = TextEditingController();
  var fromDate = TextEditingController(text: _startTime);
  var toDate = TextEditingController(text: _endTime);
  var address = TextEditingController(text: _address);
  var cityId = TextEditingController();
  var image = TextEditingController(text: _image);
  final _formKey = GlobalKey<FormState>();

  //Upload image

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;

    return Scaffold(
      body: SingleChildScrollView(
        child: Container(
          height: size.height,
          color: primaryblue,
          child: Padding(
            padding: const EdgeInsets.only(left: 25.0, right: 25),
            child: ListView(
              //mainAxisAlignment: MainAxisAlignment.center,
              //crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Container(
                  margin: EdgeInsets.only(top: 30),
                  child: Text(
                    'Safe Tech',
                    style: GoogleFonts.poppins(
                        fontStyle: FontStyle.normal,
                        fontSize: size.height / 20,
                        fontWeight: FontWeight.w600,
                        textStyle: TextStyle(color: Colors.white)),
                    textAlign: TextAlign.center,
                  ),
                ),
                SizedBox(
                  height: 20,
                ),
                Align(
                  alignment: Alignment.bottomCenter,
                  child: Container(
                    //height: 30,
                    width: size.width - 20,
                    decoration: BoxDecoration(color: primaryblue),
                    child: Column(
                      key: _formKey,
                      children: [
                        Text(
                          'Create Request',
                          style: GoogleFonts.poppins(
                              fontStyle: FontStyle.normal,
                              fontSize: size.height / 35,
                              fontWeight: FontWeight.w600,
                              textStyle: TextStyle(color: Colors.white)),
                          textAlign: TextAlign.center,
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 45,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: name,
                              onChanged: ((String name) {
                                setState(() {
                                  _name = name;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "Name",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: cnic,
                              onChanged: ((String cnic) {
                                setState(() {
                                  _cnic = cnic;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "CNIC",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 60,
                          child: Theme(
                            data: new ThemeData(
                                primaryColor: Colors.black,
                                primaryColorDark: Colors.black,
                                canvasColor: primaryblue),
                            child: DropdownButtonFormField(
                              value: "Islamabad",

                              items: [
                                DropdownMenuItem(
                                  child: Text("Peshawar"),
                                  value: "Peshawar",
                                ),
                                DropdownMenuItem(
                                  child: Text("Karachi"),
                                  value: "Karachi",
                                ),
                                DropdownMenuItem(
                                  child: Text("Lahore"),
                                  value: "Lahore",
                                ),
                                DropdownMenuItem(
                                  child: Text("Rawalpindi"),
                                  value: "Rawalpindi",
                                ),
                                DropdownMenuItem(
                                  child: Text("Islamabad"),
                                  value: "Islamabad",
                                ),
                                DropdownMenuItem(
                                  child: Text("Multan"),
                                  value: "Multan",
                                ),
                                DropdownMenuItem(
                                  child: Text("Quetta"),
                                  value: "Quetta",
                                )
                              ],

                              onChanged: ((value) {
                                setState(() {
                                  _city = value;
                                });
                              }),
                              //cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "City",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: address,
                              onChanged: ((String address) {
                                setState(() {
                                  _address = address;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "Address",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: location,
                              onChanged: ((String location) {
                                setState(() {
                                  _location = location;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "Location",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: description,
                              onChanged: ((String description) {
                                setState(() {
                                  _description = description;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "Description",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: fromDate,
                              onChanged: ((String fromDate) {
                                setState(() {
                                  _startTime = fromDate;
                                });
                              }),
                              cursorColor: Colors.white,
                              keyboardType: TextInputType.datetime,
                              decoration: new InputDecoration(
                                labelText: "From Date",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: fromDate,
                              onChanged: ((String toDate) {
                                setState(() {
                                  _endTime = toDate;
                                });
                              }),
                              cursorColor: Colors.white,
                              keyboardType: TextInputType.datetime,
                              decoration: new InputDecoration(
                                labelText: "To Date",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: image,
                              onChanged: ((String image) {
                                setState(() {
                                  _image = image;
                                });
                              }),
                              cursorColor: Colors.white,
                              keyboardType: TextInputType.datetime,
                              decoration: new InputDecoration(
                                labelText: "Image",
                                labelStyle: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w300,
                                    textStyle: TextStyle(color: Colors.white)),
                                fillColor: Colors.white,
                                enabledBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusedBorder: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                                focusColor: Colors.white,
                                border: new OutlineInputBorder(
                                  borderRadius: new BorderRadius.circular(10.0),
                                  borderSide: new BorderSide(
                                      color: Colors.white, width: 0.3),
                                ),
                              ),
                              style: GoogleFonts.poppins(
                                  fontStyle: FontStyle.normal,
                                  fontSize: size.height / 50,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        /* InkWell(
                          onTap: () async {
                            print("Uploading....");
                            chooseImage();
                            Navigator.push(context, new MaterialPageRoute(
                                builder: (BuildContext context) {
                              return DashboardPage();
                            }));
                          },
                          child: Container(
                            width: size.width,
                            height: size.height / 15,
                            decoration: BoxDecoration(
                                color: Colors.white,
                                borderRadius: BorderRadius.circular(10)),
                            child: Center(
                              child: Text(
                                'UPLOAD IMAGE',
                                style: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 30,
                                    fontWeight: FontWeight.w500,
                                    textStyle: TextStyle(color: primaryblue)),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ),
                        ),*/
                        SizedBox(
                          height: 20,
                        ),
                        InkWell(
                          onTap: () async {
                            print("Posting data....");
                            await createCustomFeedRequest()
                                .then((value) => print(value));
                            Navigator.push(context, new MaterialPageRoute(
                                builder: (BuildContext context) {
                              return RequestSubmitPage();
                            }));
                          },
                          child: Container(
                            width: size.width,
                            height: size.height / 15,
                            decoration: BoxDecoration(
                                color: Colors.white,
                                borderRadius: BorderRadius.circular(10)),
                            child: Center(
                              child: Text(
                                'SUBMIT',
                                style: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 30,
                                    fontWeight: FontWeight.w500,
                                    textStyle: TextStyle(color: primaryblue)),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        InkWell(
                          onTap: () {
                            Navigator.push(context, new MaterialPageRoute(
                                builder: (BuildContext context) {
                              return DashboardPage();
                            }));
                          },
                          child: Container(
                            width: size.width,
                            height: size.height / 15,
                            decoration: BoxDecoration(
                                color: Colors.white,
                                borderRadius: BorderRadius.circular(10)),
                            child: Center(
                              child: Text(
                                'Go Back to Home',
                                style: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 40,
                                    fontWeight: FontWeight.w500,
                                    textStyle: TextStyle(color: primaryblue)),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
