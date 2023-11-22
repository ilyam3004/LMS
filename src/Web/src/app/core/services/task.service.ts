import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private http: HttpClient) { }

  getLecturerTasks() {
    return this.http.get(`${environment.apiBaseUrl}/tasks/lecturer`)
  }

  getStudentTasks() {
    return this.http.get(`${environment.apiBaseUrl}/tasks/student`)
  }
}
