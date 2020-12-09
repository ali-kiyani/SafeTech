import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'package:safe_tech/Pages/dashboardpage.dart';
import 'package:safe_tech/utils/colors.dart';
import 'dart:io';

String _email = "user@safetech.com";
String _password = "admin4321";

class SignInPage extends StatefulWidget {
//  var cameras;

  //SignInPage(this.cameras);
  @override
  _SignInPageState createState() => _SignInPageState();
}

class _SignInPageState extends State<SignInPage> {
  var email = TextEditingController(text: "user@safetech.com");
  var password = TextEditingController(text: "admin4321");
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
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
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
                    // height: 30,
                    width: size.width - 20,
                    decoration: BoxDecoration(color: primaryblue),
                    child: Column(
                      children: [
                        Text(
                          'Login to your account',
                          style: GoogleFonts.poppins(
                              fontStyle: FontStyle.normal,
                              fontSize: size.height / 30,
                              fontWeight: FontWeight.w600,
                              textStyle: TextStyle(color: Colors.white)),
                          textAlign: TextAlign.center,
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        Container(
                          height: 50,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: TextFormField(
                              controller: email,
                              onChanged: ((String email) {
                                setState(() {
                                  _email = email;
                                });
                              }),
                              cursorColor: Colors.white,
                              decoration: new InputDecoration(
                                labelText: "Email",
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
                                  fontSize: size.height / 40,
                                  fontWeight: FontWeight.w300,
                                  textStyle: TextStyle(color: Colors.white)),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        Container(
                          height: 50,
                          width: size.width,
                          child: Theme(
                            data: new ThemeData(
                              primaryColor: Colors.white,
                              primaryColorDark: Colors.white,
                            ),
                            child: Expanded(
                              flex: 4,
                              child: Container(
                                child: TextFormField(
                                  controller: password,
                                  onChanged: ((String password) {
                                    setState(() {
                                      _password = password;
                                    });
                                  }),
                                  obscureText: true,
                                  keyboardType: TextInputType.text,
                                  cursorColor: Colors.white,
                                  decoration: new InputDecoration(
                                    labelText: "Password",
                                    labelStyle: GoogleFonts.poppins(
                                        fontStyle: FontStyle.normal,
                                        fontSize: size.height / 40,
                                        fontWeight: FontWeight.w300,
                                        textStyle:
                                            TextStyle(color: Colors.white)),
                                    fillColor: Colors.white,
                                    enabledBorder: new OutlineInputBorder(
                                      borderRadius: new BorderRadius.only(
                                          topRight: Radius.circular(10),
                                          bottomRight: Radius.circular(10)),
                                      borderSide: new BorderSide(
                                          color: Colors.white, width: 0.3),
                                    ),
                                    focusedBorder: new OutlineInputBorder(
                                      borderRadius: new BorderRadius.only(
                                          topRight: Radius.circular(10),
                                          bottomRight: Radius.circular(10)),
                                      borderSide: new BorderSide(
                                          color: Colors.white, width: 0.3),
                                    ),
                                    focusColor: Colors.white,
                                    border: new OutlineInputBorder(
                                      borderRadius: new BorderRadius.only(
                                          topRight: Radius.circular(10),
                                          bottomRight: Radius.circular(10)),
                                      borderSide: new BorderSide(
                                          color: Colors.white, width: 0.3),
                                    ),
                                  ),
                                  style: GoogleFonts.poppins(
                                      fontStyle: FontStyle.normal,
                                      fontSize: size.height / 40,
                                      fontWeight: FontWeight.w300,
                                      textStyle:
                                          TextStyle(color: Colors.white)),
                                ),
                              ),
                            ),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        Text(
                          'Enter your Email Address and the password ',
                          style: GoogleFonts.poppins(
                              fontStyle: FontStyle.normal,
                              fontSize: size.height / 50,
                              fontWeight: FontWeight.w300,
                              textStyle: TextStyle(color: Colors.white)),
                          textAlign: TextAlign.center,
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
                                'LOGIN',
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
                          height: 30,
                        ),
                        Text(
                          'For testing purposes all the fields are auto-filled. So you can easily navigate to see the end-to-end working of the app',
                          style: GoogleFonts.poppins(
                              fontStyle: FontStyle.normal,
                              fontSize: size.height / 65,
                              fontWeight: FontWeight.w300,
                              textStyle: TextStyle(color: Colors.white)),
                          textAlign: TextAlign.center,
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
