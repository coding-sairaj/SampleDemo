import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { FilterEnum } from "src/app/todos/types/filter.enum";
import { TodoInterface } from "src/app/todos/types/todo.interface";

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };
@Injectable()
export class TodosService
{
    private readonly _http: HttpClient;
    private readonly _baseUrl:string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl + 'weatherforecast/';
      }
    todos$ = new BehaviorSubject<TodoInterface[]>([]);
    filter$ = new BehaviorSubject<FilterEnum>(FilterEnum.all);

    getTodos(): void {
        this._http.get<TodoInterface[]>(this._baseUrl).subscribe(result => {
            //this.todos$.next(result);
            const tasks: TodoInterface[] = result.map(task=> {
                return <TodoInterface>{
                    id: task.id,
                    isComplete: task.isComplete,
                    task: task.task
                }
            });
            this.todos$.next(tasks);
        });
    }

    addTodo(text: string): void {
        this._http.post<TodoInterface>(this._baseUrl,{"task":text},httpOptions).subscribe(result => {
            const newTodo: TodoInterface = {
                id: result.id,
                isComplete: result.isComplete,
                task: result.task
            };
            const updateTodos = [...this.todos$.getValue(), newTodo];
            this.todos$.next(updateTodos);
          });
    }
    toggleAll(isComplete:boolean) : void {
        const updateTodos = this.todos$.getValue().map(todo => {
            const task : TodoInterface = {
                ...todo,
                isComplete
            };
            this._http.put(this._baseUrl + task.id, task).subscribe();
            return task;
        });
        this.todos$.next(updateTodos);
    }
    changeFilter(filterName:FilterEnum) : void {
        this.filter$.next(filterName);
    }
    changeTodo(id:string, text: string) : void {
        const updateTodos = this.todos$.getValue().map(todo => {
            if(todo.id === id) {
                const task : TodoInterface = {
                    ...todo,
                    task: text
                };
                this._http.put(this._baseUrl + id, task).subscribe();
                return task;
            }
            return todo;
        });
        this.todos$.next(updateTodos);
    }
    removeTodo(id:string) : void{
        this._http.delete(this._baseUrl + id).subscribe();
        const updateTodos = this.todos$.getValue().filter((todo)=>todo.id !== id);
        this.todos$.next(updateTodos);
    }
    toggleTodo(id:string) : void{
        const updateTodos = this.todos$.getValue().map(todo => {
            if(todo.id === id)
            {
                const task : TodoInterface= {
                    ...todo,
                    isComplete: !todo.isComplete
                };
                this._http.put(this._baseUrl + id, task).subscribe();
                return task;
            }
            return todo;
        });
        this.todos$.next(updateTodos);
    }
}