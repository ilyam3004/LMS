import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { AlertService } from '../../../core/services/alert.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  studentForm!: FormGroup;
  lecturerForm!: FormGroup;
  role: string = 'student';
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
    this.initializeForms();
  }



  get studentFormControl() { return this.studentForm.controls; }
  get lectuerFormControl() { return this.lecturerForm.controls; }

  onStudentFormSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.studentForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.registerLecturer(this.studentForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['../home'], { relativeTo: this.route });
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

  onLecturerFormSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.studentForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.registerLecturer(this.studentForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['../home'], { relativeTo: this.route });
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

  private initializeForms() {
    this.studentForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address: ['', Validators.required],
      course: ['1', Validators.required],
      birthday: ['', Validators.required],
      groupName: ['', Validators.required]
    });

    this.lecturerForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      degree: [''],
      birthDay: ['', Validators.required],
      address: ['', Validators.required]
    });
  }
}