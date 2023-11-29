import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {CreateSubjectRequest, LecturerSubject, StudentSubject, SubjectGrades} from "../models/subject";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpClient) { }

  getLecturerSubjects(): Observable<LecturerSubject[]> {
    return this.http.get<LecturerSubject[]>(`${environment.apiBaseUrl}/subjects/lecturer`)
  }

  getStudentSubjects(): Observable<StudentSubject[]> {
    return this.http.get<StudentSubject[]>(`${environment.apiBaseUrl}/subjects/student`)
  }

  getLecturerGrades(): Observable<SubjectGrades[]> {
    return this.http.get<SubjectGrades[]>(`${environment.apiBaseUrl}/grades`);
  }

  createSubject(request: CreateSubjectRequest): Observable<LecturerSubject[]> {
    return this.http.post<LecturerSubject[]>(`${environment.apiBaseUrl}/subjects`, request);
  }

  removeSubject(subjectId: string): Observable<LecturerSubject[]> {
    return this.http.delete<LecturerSubject[]>(`${environment.apiBaseUrl}/subjects/${subjectId}`);
  }
}
