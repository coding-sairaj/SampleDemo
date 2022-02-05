import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { FilterEnum } from "src/app/todos/types/filter.enum";
import { TodoInterface } from "src/app/todos/types/todo.interface";

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: 'my-auth-token'
    })
  };
@Injectable()
export class TodosService
{
    private readonly _http: HttpClient;
    private readonly _baseUrl:string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl + 'weatherforecast';
      }
    todos$ = new BehaviorSubject<TodoInterface[]>([]);
    filter$ = new BehaviorSubject<FilterEnum>(FilterEnum.all);

    

    addTodo(text: string): void {
        this._http.post<string>(this._baseUrl,text).subscribe(result => {
            
          });
        const newTodo: TodoInterface = {
            text,
            isCompleted: false,
            id: Math.random().toString(16)
        };
        const updateTodos = [...this.todos$.getValue(), newTodo];
        this.todos$.next(updateTodos);
    }
    toggleAll(isCompleted:boolean) : void {
        const updateTodos = this.todos$.getValue().map(todo => {
            return {
                ...todo,
                isCompleted
            }
        });
        this.todos$.next(updateTodos);
    }
    changeFilter(filterName:FilterEnum) : void {
        this.filter$.next(filterName);
    }
    changeTodo(id:string, text: string) : void {
        const updateTodos = this.todos$.getValue().map(todo => {
            if(todo.id === id) {
                return {
                    ...todo,
                    text,
                };
            }
            return todo;
        });
        this.todos$.next(updateTodos);
    }
    removeTodo(id:string) : void{
        const updateTodos = this.todos$.getValue().filter((todo)=>todo.id !== id);
        this.todos$.next(updateTodos);
    }
    toggleTodo(id:string) : void{
        const updateTodos = this.todos$.getValue().map(todo => {
            if(todo.id === id)
            {
                return {
                    ...todo,
                    isCompleted: !todo.isCompleted
                };
            }
            return todo;
        });
        this.todos$.next(updateTodos);
    }
}