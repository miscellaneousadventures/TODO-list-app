import { Component, signal, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgClass } from '@angular/common';
import { TodolistService, TodoListItem } from './todolist.service';
import { switchMap } from 'rxjs';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [RouterOutlet, NgClass]
})
export class AppComponent implements OnInit {
  

  private todolistService = inject(TodolistService);
  todoList = signal<TodoListItem[]>([]);
  newTodoListItem = signal('');

  ngOnInit(): void {
    this.todolistService.getAllTodoListItems().subscribe({
      next: (data) => this.todoList.set(data),
      error: (err) => console.error('Failed to fetch todo items', err)
    });
  }



  addTask(task: string): void {
    if (!task.trim()) return;
    this.todolistService.addTodoListItem(task).pipe(
      switchMap(() => this.todolistService.getAllTodoListItems())
    ).subscribe({
      next: (data) => {
        this.todoList.set(data);
        this.newTodoListItem.set('');
      },
      error: (err) => console.error('Failed to add todo item', err)
    });
  }

  // better to use debounce
  toggleCompleted(id: Guid): void {
    const item = this.todoList().find(item => item.id === id);
    if (!item) return;
    this.todolistService.updateTodoListItem(id, { ...item, isCompleted: !item.isCompleted })
      .pipe(switchMap(() => this.todolistService.getAllTodoListItems()))
      .subscribe({
        next: data => this.todoList.set(data),
        error: err => console.error('Failed to update todo item', err)
      });
  }

  deleteTask(id: Guid): void {
    this.todolistService.deleteTodoListItem(id).pipe(
      switchMap(() => this.todolistService.getAllTodoListItems())
    ).subscribe({
      next: (data) => this.todoList.set(data),
      error: (err) => console.error('Failed to delete todo item', err)
    });
  }
}
