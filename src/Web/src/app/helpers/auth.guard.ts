import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../core/services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = this.authenticationService.userValue;
    this.authenticationService.logout(); 
    if (user) {
      return true;
    }

    this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}