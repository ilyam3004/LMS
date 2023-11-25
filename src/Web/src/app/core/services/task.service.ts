import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AssignTaskRequest, LecturerTask} from "../models/task";
import {Observable} from "rxjs";
import {LecturerSubject} from "../models/subject";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private http: HttpClient) { }

  assignTask(request: AssignTaskRequest): Observable<LecturerSubject>{
    return this.http.post<LecturerSubject>(`${environment.apiBaseUrl}/tasks`, request);
  }

  getTask(taskId: string): Observable<LecturerTask>{
    return this.http.get<LecturerTask>(`${environment.apiBaseUrl}/tasks/${taskId}`);
  }

  removeTask(taskId: string): Observable<LecturerSubject>{
    return this.http.delete<LecturerSubject>(`${environment.apiBaseUrl}/tasks/${taskId}`);
  }

  getStudentTasks() {
    return this.http.get(`${environment.apiBaseUrl}/tasks/student`)
  }
}
