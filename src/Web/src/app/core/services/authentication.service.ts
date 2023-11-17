import {BehaviorSubject, Observable, map} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {RegisterLecturerRequest, RegisterStudentRequest, User} from '../models/user';
import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private userSubject: BehaviorSubject<User | null>;
  public user: Observable<User | null>;

  constructor(private router: Router,
              private http: HttpClient) {
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  public get userValue() {
    return this.userSubject.value;
  }

  login(email: string, password: string) {
    return this.http.post<User>(`${environment.apiBaseUrl}/users/login`, {email, password})
      .pipe(map(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  registerLecturer(request: RegisterLecturerRequest) {
    return this.http.post(`${environment.apiBaseUrl}/users/lecturers/register`, request)
      .pipe(map(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  registerStudent(request: RegisterStudentRequest) {
    return this.http.post(`${environment.apiBaseUrl}/users/students/register`, request)
      .pipe(map(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  getUserRole(token: string): string | null {
    const jwtService = new JwtHelperService();
    const decodedToken = jwtService.decodeToken(token);
    return decodedToken['role'];
  }
}
