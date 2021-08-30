import { Body, Controller, Get, Post } from '@nestjs/common';
import { Todo } from './todo.interface';
import { TodosService } from './todos.service';

@Controller('todos')
export class TodosController {
  constructor(private _todosService: TodosService) {}

  @Post()
  async create(@Body() todo: Todo) {
      this._todosService.create(todo);
  }

  @Get()
  async findAll(): Promise<Todo[]>  {
    return this._todosService.findAll();
  }
}
