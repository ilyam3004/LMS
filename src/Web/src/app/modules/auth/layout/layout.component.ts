import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';

@Component({ templateUrl: './layout.component.html', })
export class LayoutComponent {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    if (this.authenticationService.userValue) {
      this.router.navigate(['/home']);
    }
  }
}