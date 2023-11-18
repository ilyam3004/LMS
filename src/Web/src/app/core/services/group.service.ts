import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Group} from "../models/group";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private http: HttpClient) { }

  getAllGroups(): Observable<Group[]>{
    return this.http.get<Group[]>(`${environment.apiBaseUrl}/groups`);
  }
}
