import 'package:flutter/material.dart';

class InputField extends StatelessWidget {
  final String label;
  final void Function(String) onChanged;

  const InputField({super.key, 
    required this.label,
    required this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: 8),
      child: TextFormField(
        decoration: InputDecoration(
          labelText: label,
          border: OutlineInputBorder(),
        ),
        onChanged: onChanged,
        validator: (val) =>
            (val == null || val.isEmpty) ? '$label is required' : null,
      ),
    );
  }
}
