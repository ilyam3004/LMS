import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AssignTaskRequest, LecturerTask} from "../models/task";
import {Observable} from "rxjs";
import {LecturerSubject} from "../models/subject";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  taskApiUrl: string = `${environment.apiBaseUrl}/tasks`;
  constructor(private http: HttpClient) { }

  assignTask(request: AssignTaskRequest): Observable<LecturerSubject> {
    return this.http.post<LecturerSubject>(this.taskApiUrl, request);
  }

  getTask(taskId: string): Observable<LecturerTask> {
    return this.http.get<LecturerTask>(`${this.taskApiUrl}/${taskId}`);
  }

  removeTask(taskId: string): Observable<LecturerSubject> {
    return this.http.delete<LecturerSubject>(`${this.taskApiUrl}/${taskId}`);
  }

  returnTaskToStudent(studentTaskId: string): Observable<LecturerTask> {
    return this.http.put<LecturerTask>(`${this.taskApiUrl}/${studentTaskId}/return`, null);
  }

  acceptTask(studentTaskId: string, grade: number): Observable<LecturerTask> {
    return this.http.put<LecturerTask>(`${this.taskApiUrl}/${studentTaskId}/accept`,
      {grade: grade});
  }

  getStudentTasks() {
    return this.http.get(`${this.taskApiUrl}/student`)
  }
}
