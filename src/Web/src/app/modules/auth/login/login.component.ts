import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "../../../core/services/alert.service";
import {AuthenticationService} from "../../../core/services/authentication.service";
import {User} from "../../../core/models/user";

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
        error: (error) => {
          if(error.status == 400){
            this.alertService.error(error.error.errors?.Email
              ? error.error.errors?.Email[0]
              : error.error.errors?.Password[0]);
          }
          else {
            this.alertService.error(error.error.title);
          }
          this.loading = false;
        }
      });
  }
}
