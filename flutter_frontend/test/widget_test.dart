// This is a basic Flutter widget test.
//
// To perform an interaction with a widget in your test, use the WidgetTester
// utility in the flutter_test package. For example, you can send tap and scroll
// gestures. You can also use WidgetTester to find child widgets in the widget
// tree, read text, and verify that the values of widget properties are correct.


import 'package:flutter_test/flutter_test.dart';
import 'package:flutter_frontend/main.dart';

void main() {
  testWidgets('Home page shows Create User button', (WidgetTester tester) async {

    await tester.pumpWidget(MyApp());

    await tester.pumpAndSettle();

    // Verify that the "Create User" button is present
    expect(find.text('Create User'), findsOneWidget);

    // Press(tap) the button to navigate
    await tester.tap(find.text('Create User'));
    await tester.pumpAndSettle();


    expect(find.text('Submit'), findsOneWidget);
  });
}
