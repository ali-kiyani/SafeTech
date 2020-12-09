import 'package:flutter/material.dart';
import 'package:safe_tech/Pages/livefeedpage.dart';
import 'package:google_fonts/google_fonts.dart';

import 'package:safe_tech/Pages/materialpage.dart';
import 'package:safe_tech/utils/colors.dart';

import 'package:safe_tech/utils/customtext.dart';

class DashboardPage extends StatefulWidget {
  // var cameras;

  //DashboardPage(this.cameras);
  @override
  _DashboardPageState createState() => _DashboardPageState();
}

class _DashboardPageState extends State<DashboardPage> {
  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;

    Orientation currentOrientation = MediaQuery.of(context).orientation;

    double fontNormal, fontHeading;

    if (currentOrientation == Orientation.portrait) {
      fontNormal = size.height / 50;
      fontHeading = size.height / 15;
    } else {
      fontNormal = size.height / 20;
      fontHeading = size.height / 10;
    }

    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          child: Container(
            height: size.height,
            width: size.width,
            color: primaryblue,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Container(
                  height: 80,
                  margin: EdgeInsets.only(top: 10),
                  child: Center(
                    child: Text(
                      'Safe Tech',
                      style: GoogleFonts.poppins(
                          fontStyle: FontStyle.normal,
                          fontSize: fontHeading,
                          fontWeight: FontWeight.w400,
                          textStyle: TextStyle(color: Colors.white)),
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
                Expanded(
                  child: Align(
                    alignment: Alignment.bottomCenter,
                    child: Container(
                      width: size.width,
                      decoration: BoxDecoration(
                          borderRadius: BorderRadius.only(
                              topRight: Radius.circular(20),
                              topLeft: Radius.circular(20)),
                          color: Colors.white70),
                      child: Padding(
                        padding: const EdgeInsets.all(15.0),
                        child: Column(
                          children: [
                            IntrinsicHeight(),
                            SizedBox(
                              height: 200,
                            ),
                            InkWell(
                              onTap: () {
                                Navigator.push(context, new MaterialPageRoute(
                                    builder: (BuildContext context) {
                                  return MaterialSamplePage();
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
                                    'Create Request',
                                    style: GoogleFonts.poppins(
                                        fontStyle: FontStyle.normal,
                                        fontSize: size.height / 40,
                                        fontWeight: FontWeight.w500,
                                        textStyle:
                                            TextStyle(color: primaryblue)),
                                    textAlign: TextAlign.center,
                                  ),
                                ),
                              ),
                            ),
                            SizedBox(
                              height: 60,
                            ),
                            InkWell(
                              onTap: () {
                                Navigator.push(context, new MaterialPageRoute(
                                    builder: (BuildContext context) {
                                  return LiveFeedPage();
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
                                    'View Live Feed',
                                    style: GoogleFonts.poppins(
                                        fontStyle: FontStyle.normal,
                                        fontSize: size.height / 40,
                                        fontWeight: FontWeight.w500,
                                        textStyle:
                                            TextStyle(color: primaryblue)),
                                    textAlign: TextAlign.center,
                                  ),
                                ),
                              ),
                            ),
                            Container(
                              margin: EdgeInsets.only(top: 10),
                              child: Text(
                                "",
                                style: GoogleFonts.poppins(
                                    fontStyle: FontStyle.normal,
                                    fontSize: size.height / 50,
                                    fontWeight: FontWeight.w500,
                                    textStyle: TextStyle(color: Colors.black)),
                                textAlign: TextAlign.center,
                              ),
                            )
                          ],
                        ),
                      ),
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
