import 'package:flutter/material.dart';
import '../widgets/input_field.dart';
import '../services/api_client.dart';

class CreateUserPage extends StatefulWidget {
  const CreateUserPage({super.key});

  @override
  State<CreateUserPage> createState() => _CreateUserPageState();
}

class _CreateUserPageState extends State<CreateUserPage> {
  final _formKey = GlobalKey<FormState>();
  final _api = ApiClient();

  String firstName = '', lastName = '', email = '', license = '', mobile = '';
  bool isLoading = false;
  String? resultMessage;

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;
    setState(() { isLoading = true; resultMessage = null; });

    final data = {
      'firstName': firstName,
      'lastName':  lastName,
      'email':     email,
      'license':   license,
    };

    try {
      final resp = await _api.createUser(data);
      if (resp.statusCode == 201) {
        setState(() => resultMessage = 'User created!');
      } else if (resp.statusCode == 409) {
        setState(() => resultMessage = 'User already exists.');
      } else {
        setState(() => resultMessage = 'Error: ${resp.body}');
      }
    } catch (e) {
      setState(() => resultMessage = 'Network error');
    } finally {
      setState(() { isLoading = false; });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Create User')),
      body: Padding(
        padding: EdgeInsets.all(16),
        child: Column(
          children: [
            if (resultMessage != null)
              Text(
                resultMessage!,
                style: TextStyle(
                  color: resultMessage!.startsWith('Error') ? Colors.red : Colors.green
                ),
              ),
            Expanded(
              child: Form(
                key: _formKey,
                child: ListView(
                  children: [
                    InputField(label: 'First Name', onChanged: (v) => firstName = v),
                    InputField(label: 'Last Name',  onChanged: (v) => lastName = v),
                    InputField(label: 'Email',      onChanged: (v) => email = v),
                    InputField(label: 'License',    onChanged: (v) => license = v),
                    InputField(label: 'Mobile',     onChanged: (v) => mobile = v),
                  ],
                ),
              ),
            ),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: isLoading ? null : _submit,
                child: isLoading
                    ? CircularProgressIndicator(color: Colors.white)
                    : Text('Submit'),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
