import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "../../../core/services/alert.service";
import {AuthenticationService} from "../../../core/services/authentication.service";
import {User} from "../../../core/models/user";
import {Error} from "../../../core/models/error";
import {catchError, first} from "rxjs";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get loginFormControl() {
    return this.loginForm.controls;
  }

  onSubmit() {
    console.log(this.loginForm.errors)
    this.submitted = true;

    this.alertService.clear();

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.loginFormControl['email'].value,
      this.loginFormControl['password'].value)
      .subscribe({
        next: (user: User) => {
          const role = this.authenticationService.getUserRole(user.token!);
          if(role == 'Lecturer') {
            this.router.navigate(['/lecturer/subjects'], { relativeTo: this.route });
          } else if (role == 'Student') {
            this.router.navigate(['/student/subjects'], { relativeTo: this.route });
          }
        },
        error: (error: Error) => {
          this.alertService.error(error?.title)
          this.loading = false;
        }
      });
  }
}
