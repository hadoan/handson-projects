import 'package:flutter/material.dart';

class PromptRemoveTask {
  static Widget Build(BuildContext context, List<String> _todoItems, int index){
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
             _todoItems.removeAt(index);
            Navigator.of(context).pop();
          },
        )
      ],
    );
  }
}  
