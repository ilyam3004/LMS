import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {AuthenticationService} from '../core/services/authentication.service';

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = this.authenticationService.userValue;

    if (user) {
      const expectedRole = route.data['expectedRole'];
      const userRole = this.authenticationService.getUserRole(user.token!);
      if(userRole && userRole == expectedRole){
        return true;
      }
    }

    this.router.navigate(['/account/login'], {queryParams: {returnUrl: state.url}});
    return false;
  }
}
