import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiClient {
  final _baseUrl = 'http://localhost:3000/api';

  Future<http.Response> createUser(Map<String, String> data) {
    return http.post(
      Uri.parse('$_baseUrl/users'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(data),
    );
  }
}
