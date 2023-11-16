import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../core/services/authentication.service';
import { environment } from '../../environments/environment';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const user = this.accountService.userValue;
        const isLoggedIn = user && user.token;
        const isApiUrl = request.url.startsWith(environment.apiBaseUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.token}`
                }
            });
        }

        return next.handle(request);
    }
}