import 'package:flutter/material.dart';
import 'pages/home_page.dart';
import 'pages/create_user_page.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'CarRental Client',
      theme: ThemeData(primarySwatch: Colors.blue),
      initialRoute: '/',
      routes: {
        '/':            (_) => HomePage(),
        '/create_user': (_) => CreateUserPage(),
      },
    );
  }
}
