import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "../../../core/services/alert.service";
import {AuthenticationService} from "../../../core/services/authentication.service";
import {first} from "rxjs";
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
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get loginFormControl() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    console.log(this.loginFormControl['email'].value)
    this.authenticationService.login(this.loginFormControl['email'].value,
      this.loginFormControl['password'].value)
      .pipe(first())
      .subscribe({
        next: (user: User) => {
          const role = this.authenticationService.getUserRole(user.token!);
          if(role == 'Lecturer') {
            this.router.navigate(['/lecturer'], { relativeTo: this.route });
          } else if (role == 'Student') {
            this.router.navigate(['/student'], { relativeTo: this.route });
          }
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
