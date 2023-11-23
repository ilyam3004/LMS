import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AssignTaskRequest} from "../models/task";
import {Observable} from "rxjs";
import {Subject} from "../models/subject";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private http: HttpClient) { }

  assignTask(request: AssignTaskRequest){
    return this.http.post(`${environment}/tasks`, request);
  }

  removeTask(taskId: string): Observable<Subject>{
    return this.http.delete<Subject>(`${environment.apiBaseUrl}/tasks/${taskId}`);
  }

  getStudentTasks() {
    return this.http.get(`${environment.apiBaseUrl}/tasks/student`)
  }
}
