import { HttpClient } from '@angular/common/http';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { environment } from '../environments/environment.development';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
// import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  template: `
      <h1>Todo 📃</h1>

      @if(loading){
        <div>🔃Loading....</div>
      }

      @if(error){
        {{error}}
      }

      <div style="margin-bottom:1.2rem; display:flex; gap:3px">
          <input type="text" id="title" [(ngModel)]="title"/> 
          <button type="button" (click)="addTodo()">➕</button>
      </div>
      
      @if(todos && todos.length>0){
        <ul>
          @for(todo of todos;track todo.id){
           <li [style.text-decoration]="todo.completed?'line-through':'none'" > 
             <input type="checkbox" [checked]="todo.completed" (click)="toggleTodo(todo)">
             {{todo.title}} | <button (click)="deleteTodo(todo)">❌</button>
            </li>
          } 
        </ul>
      }
      @else {
        <div>No todos</div>
      }
      `,
  styles: [],
})
export class App implements OnInit {
  private baseApiUrl = environment.API_BASE_URL + "/api/todos";
  private http = inject(HttpClient);
  private destroyRef = inject(DestroyRef);

  todos: TodoModel[] = [];
  error = "";
  loading = true;
  title = "";

  addTodo = () => {
    this.loading = true;
    const todo: TodoModel = {
      title: this.title,
      completed: false
    }
    this.http.post<TodoModel>(this.baseApiUrl, todo)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (todo) => {
          this.todos.push(todo);
        },
        error: (error) => {
          this.error = "Something went wrong";
          console.log(error);
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
          this.title = "";
        }
      });
  }

  toggleTodo = (todo: TodoModel) => {
    todo.completed = !todo.completed;
    this.loading = true;
    this.http.put(this.baseApiUrl, todo).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.todos = this.todos.map(a => a.id === todo.id ? todo : a);
      },
      error: (error) => {
        error = "Error..";
        console.log(error);
        this.loading = false;
      },
      complete: () => this.loading = false
    });
  }

  deleteTodo = (todo: TodoModel) => {
    const sure = confirm("Are you sure???")
    if (!sure) return;

    this.http.delete(`${this.baseApiUrl}/${todo.id}`)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.todos = this.todos.filter(t => t.id !== todo.id);
        },
        error: (err) => {
          this.error = "Error..";
          console.log(err);
          this.loading = false
        },
        complete: () => {
          this.loading = false
        }
      });
  }

  ngOnInit(): void {
    this.loading = true;
    this.http.get<TodoModel[]>(this.baseApiUrl).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (data) => {
        this.todos = data;
      },
      error: (error) => {
        this.error = "Something went wrong";
        console.log(error);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
}

export interface TodoModel {
  id?: string | null,
  title: string,
  completed: boolean
}