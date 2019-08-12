import 'package:flutter/material.dart';
import 'package:flutter_todo/TodoListWidget.dart';

void main() => runApp(TodoApp());

class TodoApp extends StatelessWidget{

  

  @override
  Widget build(BuildContext context){

    return new MaterialApp(
      title: "My Todo List",
      home: new TodoListWidget()
    );

  

  }
}
