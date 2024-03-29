import { Component, OnInit } from '@angular/core';
import { combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TodosService } from '../../services/todos.service';
import { FilterEnum } from '../../types/filter.enum';
import { TodoInterface } from '../../types/todo.interface';

@Component({
  selector: 'app-todos-main',
  templateUrl: './main.component.html'
})
export class MainComponent implements OnInit {
  isAllTodosChecked$: Observable<boolean>;
  visibleTodos$: Observable<TodoInterface[]>;
  noTodoClass$: Observable<boolean>;
  editingId: string | null = null;

  constructor(private todoService: TodosService) {
    this.isAllTodosChecked$ =  this.todoService.todos$.pipe(
      map((todos => todos.every(todo => todo.isComplete)))
    );
    this.noTodoClass$ = this.todoService.todos$.pipe(
      map((todos) => todos.length ===0)
    );
    this.visibleTodos$ = combineLatest([this.todoService.todos$, this.todoService.filter$]).pipe(map(([todos, filter]: [TodoInterface[], FilterEnum])=>{
      if(filter === FilterEnum.active)
      {
        return todos.filter(todo => !todo.isComplete);
      } else if(filter === FilterEnum.completed)
      {
        return todos.filter(todo => todo.isComplete);
      }
      return todos;

    }));
   }
   toggleAllTodos(event : Event) :void {
    const target = event.target as HTMLInputElement;
    this.todoService.toggleAll(target.checked);
   }
   setEditingId(editingId:string | null) : void{
     this.editingId = editingId;
   }

  ngOnInit(): void {
    this.todoService.getTodos();
  }

}
