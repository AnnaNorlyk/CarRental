import 'package:flutter/material.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('CarRental Home')),
      body: Center(
        child: ElevatedButton(
          child: Text('Create User'),
          onPressed: () => Navigator.pushNamed(context, '/create_user'),
        ),
      ),
    );
  }
}
