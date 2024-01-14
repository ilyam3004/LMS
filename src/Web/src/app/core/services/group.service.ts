import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {GroupsResponse} from "../models/group";

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private http: HttpClient) { }

  getAllGroups(): Observable<GroupsResponse>{
    return this.http.get<GroupsResponse>(`${environment.apiBaseUrl}/groups`);
  }
}
