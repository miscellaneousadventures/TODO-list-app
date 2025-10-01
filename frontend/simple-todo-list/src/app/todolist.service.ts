import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from "guid-typescript";

export interface TodoListItem {
    id: Guid;
    name: string;
    isCompleted: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TodolistService {
    private apiUrl = 'http://localhost:5159/todolistitems';
    constructor(private http: HttpClient) {}

    getAllTodoListItems(): Observable<TodoListItem[]> {
        return this.http.get<TodoListItem[]>(`${this.apiUrl}/`);
    }

    addTodoListItem(task: string): Observable<TodoListItem> {
        const newItem = { name: task, isCompleted: false };
        return this.http.post<TodoListItem>(`${this.apiUrl}/`, newItem);
    }

    deleteTodoListItem(id: Guid): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    updateTodoListItem(id: Guid, item: TodoListItem): Observable<void> {
        const updated = { name: item.name, isCompleted: item.isCompleted };
        return this.http.put<void>(`${this.apiUrl}/${id}`, updated);
    }
}    