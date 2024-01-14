import {CreateSubjectRequest, LecturerSubjectsResponse, 
  StudentSubjectsResponse, SubjectGrades} from "../models/subject";
import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpClient) { }

  getLecturerSubjects(): Observable<LecturerSubjectsResponse> {
    return this.http.get<LecturerSubjectsResponse>(`${environment.apiBaseUrl}/subjects/lecturer`)
  }

  getStudentSubjects(): Observable<StudentSubjectsResponse> {
    return this.http.get<StudentSubjectsResponse>(`${environment.apiBaseUrl}/subjects/student`)
  }

  getLecturerGrades(): Observable<SubjectGrades[]> {
    return this.http.get<SubjectGrades[]>(`${environment.apiBaseUrl}/grades`);
  }

  createSubject(request: CreateSubjectRequest): Observable<LecturerSubjectsResponse> {
    return this.http.post<LecturerSubjectsResponse>(`${environment.apiBaseUrl}/subjects`, request);
  }

  removeSubject(subjectId: string): Observable<LecturerSubjectsResponse> {
    return this.http.delete<LecturerSubjectsResponse>(`${environment.apiBaseUrl}/subjects/${subjectId}`);
  }
}