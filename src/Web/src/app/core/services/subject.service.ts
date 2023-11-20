import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {CreateSubjectRequest, Subject} from "../models/subject";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpClient) { }

  getLecturerSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${environment.apiBaseUrl}/subjects/lecturer`)
  }

  createSubject(request: CreateSubjectRequest): Observable<Subject[]> {
    return this.http.post<Subject[]>(`${environment.apiBaseUrl}/subjects`, request);
  }

  removeSubject(subjectId: string): Observable<Subject[]> {
    return this.http.delete<Subject[]>(`${environment.apiBaseUrl}/subjects/${subjectId}`);
  }
}
