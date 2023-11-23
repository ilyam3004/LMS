import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AssignTaskRequest} from "../models/task";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private http: HttpClient) { }

  assignTask(request: AssignTaskRequest){
    return this.http.post(`${environment}/tasks`, request);
  }

  getStudentTasks() {
    return this.http.get(`${environment.apiBaseUrl}/tasks/student`)
  }
}
