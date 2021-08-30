import { Inject, Injectable } from '@nestjs/common';
import { Todo } from './todo.interface';

@Injectable()
export class TodosService {
  private readonly todos: Todo[] = [];

  create(todo: Todo) {
    this.todos.push(todo);
  }

  findAll(): Todo[] {
    return this.todos;
  }
}
