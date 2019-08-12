import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import 'TodoListWidget.dart';

class TodoListState extends State<TodoListWidget>{
  List<String> _todoItems=[];
  int removingTaskIndex = -1;

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title: new Text("Todo List"),
      ),
      body: _buildTodoList(),
      floatingActionButton: _buildFloatingActionButtion() ,
    );
  }

  Widget _buildFloatingActionButtion(){
    return new FloatingActionButton(
      onPressed: (){
         Navigator.of(context).push(
          // MaterialPageRoute will automatically animate the screen entry, as well
          // as adding a back button to close it
          new MaterialPageRoute(
            builder: _buildAddTaskWidget,
          )
        );
      },
      tooltip: 'Add task',
      child: new Icon(Icons.add),
    );
  }

  Widget _buildAddTaskWidget(BuildContext context){
    return new Scaffold(
      appBar: new AppBar(
        title: new Text('Add a new task'),
      ),
      body: new TextField(
        autofocus: true,
        onSubmitted: (val){
          _addTodoItem(val);
          Navigator.pop(context);//close add task screen
        },
        decoration: new InputDecoration(
          hintText: 'Enter your task',
          contentPadding: const EdgeInsets.all(16.0)
        ),
      ),
    );
  }

  Widget _buildPromptRemoveTodoDialog(BuildContext context, int index){
    return new AlertDialog(
      title: new Text('Mark "${_todoItems[index]}" as done?'),
      actions: <Widget>[
        new FlatButton(
          child: new Text('CANCEL'),
          onPressed: ()=> Navigator.of(context).pop(),
        ),
        new FlatButton(
          child: new Text("MARK DONE"),
          onPressed: (){
            _removeTodoItem(index);
            Navigator.of(context).pop();
            this.removingTaskIndex = -1;
          },
        )
      ],
    );
  }

  Widget _buildTodoList(){
    return new ListView.builder(
      // itemBuilder will be automatically be called as many times as it takes for the
      // list to fill up its available space, which is most likely more than the
      // number of todo items we have. So, we need to check the index is OK.

      itemBuilder: (context,index){
        if(index<_todoItems.length){
          return _buildTodoItem(_todoItems[index], index);
        }
      },
    );
  }

  Widget _buildTodoItem(String todoTitle,int index){
    return new ListTile(
      title: new Text(todoTitle),
      onTap: (){
         showDialog(
          context: context,
          builder: (context){ return _buildPromptRemoveTodoDialog(context,index);}
        );
      },
    );
  }

  void _addTodoItem(String task){
    //setState tells the app that our state has changed and it will automatically re-render list
    setState((){
      // int index=_todoItems.length;
      _todoItems.add(task);
    });
  }

  void _removeTodoItem(int index){
    setState(() {
      _todoItems.removeAt(index);
    });
  }
}