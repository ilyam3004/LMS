import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AssignTaskRequest, LecturerTask, StudentTask, StudentTaskStatus, UploadedStudentTask} from "../models/task";
import {Observable} from "rxjs";
import {LecturerSubject} from "../models/subject";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private taskApiUrl: string = `${environment.apiBaseUrl}/tasks`;

  constructor(private http: HttpClient) { }

  assignTask(request: AssignTaskRequest): Observable<LecturerSubject> {
    return this.http.post<LecturerSubject>(this.taskApiUrl, request);
  }

  getLecturerTaskDetails(taskId: string): Observable<LecturerTask> {
    return this.http.get<LecturerTask>(`${this.taskApiUrl}/lecturer/${taskId}`);
  }

  getStudentTaskDetails(taskId: string): Observable<StudentTask> {
    return this.http.get<StudentTask>(`${this.taskApiUrl}/student/${taskId}`);
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

  rejectTask(studentTaskId: string): Observable<LecturerTask> {
    return this.http.put<LecturerTask>(`${this.taskApiUrl}/${studentTaskId}/reject`, null);
  }

  uploadSolutions(file: File, studentTaskId: string): Observable<StudentTask> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    return this.http.put<StudentTask>(`${this.taskApiUrl}/${studentTaskId}/upload`, formData);
  }

  removeUploadedSolution(studentTaskId: string): Observable<StudentTask> {
    return this.http.put<StudentTask>(`${this.taskApiUrl}/${studentTaskId}/remove`, null);
  }

  downloadSolution(studentTaskId: string): Observable<any> {
    return this.http.get(`${this.taskApiUrl}/${studentTaskId}/download`, {
      observe: 'response',
      responseType: 'blob'
    })
  }

  commentTask(studentTaskId: string, comment: string): Observable<UploadedStudentTask> {
    return this.http.put<UploadedStudentTask>(`${this.taskApiUrl}/${studentTaskId}/comment`,
      {comment: comment});
  }

  getTaskStatus(status: StudentTaskStatus): string {
    return status === StudentTaskStatus.Accepted ? 'Accepted' :
      status === StudentTaskStatus.Returned ? 'Returned' :
        status === StudentTaskStatus.Rejected ? 'Rejected' :
          status === StudentTaskStatus.Uploaded ? 'Turned in' : 'Not uploaded';
  }

  getTaskStatusColor(status: StudentTaskStatus): string {
    switch (status) {
      case StudentTaskStatus.Uploaded:
        return '#ffa94b';
      case StudentTaskStatus.Accepted:
        return '#00d300';
      case StudentTaskStatus.Returned:
        return 'red';
      case StudentTaskStatus.NotUploaded:
        return 'gray';
      case StudentTaskStatus.Rejected:
        return '#fa4343';
      default:
        return 'gray';
    }
  }
}
