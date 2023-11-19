import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Subject} from "../models/subject";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpClient) { }

  getLecturerSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${environment.apiBaseUrl}/subjects/lecturer`)
  }
}
