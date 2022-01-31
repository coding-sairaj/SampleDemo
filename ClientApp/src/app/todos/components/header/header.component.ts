import { Component, OnInit } from '@angular/core';
import { TodosService } from '../../services/todos.service';

@Component({
  selector: 'app-todos-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(private todoService: TodosService) {
      
   }
  text: string = '';
  changeText(event: Event)
  {
    const target = event.target as HTMLInputElement;
    this.text = target.value;
  }
  addTodo() : void
  {
    this.todoService.addTodo(this.text);
    this.text = '';
  }
  ngOnInit(): void {
  }

}
