import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { AlertService } from '../../../core/services/alert.service';
import { first } from 'rxjs';
import {RegisterLecturerRequest, RegisterStudentRequest} from "../../../core/models/user";

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
  get lecturerFormControl() { return this.lecturerForm.controls; }

  onStudentFormSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.studentForm.invalid) {
      return;
    }

    const request: RegisterStudentRequest = this.studentForm.value;
    this.loading = true;
    request.birthday = new Date(request.birthday).toISOString();

    this.authenticationService.registerStudent(this.studentForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['/student'], {relativeTo: this.route});
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

    if (this.lecturerForm.invalid) {
      return;
    }

    const request: RegisterLecturerRequest = this.lecturerForm.value;
    this.loading = true;
    request.birthday = new Date(request.birthday).toISOString();

    this.authenticationService.registerLecturer(request)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['/lecturer'], { relativeTo: this.route });
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
      course: [1, Validators.required],
      birthday: ['', Validators.required],
      groupName: ['', Validators.required]
    });

    this.lecturerForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      degree: [''],
      birthday: ['', Validators.required],
      address: ['', Validators.required]
    });
  }
}
