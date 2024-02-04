import { RegisterLecturerRequest, RegisterStudentRequest } from "../../../core/models/user";
import { AuthenticationService } from '../../../core/services/authentication.service';
import { AlertService } from '../../../core/services/alert.service';
import { GroupService } from "../../../core/services/group.service";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Group, GroupsResponse } from "../../../core/models/group";
import { Component, OnInit } from '@angular/core';
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
  groups: Group[] = [];
  groupLoading = false;
  registerLoading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private groupService: GroupService
  ) { }

  ngOnInit() {
    this.fetchGroups();
  }

  get studentFormControl() {
    return this.studentForm.controls;
  }

  get lecturerFormControl() {
    return this.lecturerForm.controls;
  }

  onStudentFormSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.studentForm.invalid) {
      return;
    }
    this.registerLoading = true;

    const request: RegisterStudentRequest = this.studentForm.value;
    const selectedDate = this.studentForm.value.birthday;
    request.birthday = new Date(selectedDate.year,
      selectedDate.month - 1, selectedDate.day).toISOString();

    console.log(request);

    this.authenticationService.registerStudent(request)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['/student/subjects'], { relativeTo: this.route });
        },
        error: error => {
          this.alertService.error(error.error.message);
          this.registerLoading = false;
          if (error.status == 400) {
            this.studentForm.reset();
          }
        }
      });
  }

  onLecturerFormSubmit() {
    this.submitted = true;

    this.alertService.clear();
    if (this.lecturerForm.invalid) {
      return;
    }
    this.registerLoading = true;

    const request: RegisterLecturerRequest = this.lecturerForm.value;
    const selectedDate = this.lecturerForm.value.birthday;
    request.birthday = new Date(selectedDate.year,
      selectedDate.month - 1, selectedDate.day).toISOString();

    this.authenticationService.registerLecturer(request)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['/lecturer/subjects'], { relativeTo: this.route });
          this.lecturerForm.reset();
        },
        error: error => {
          this.alertService.error(error.error.message);
          this.registerLoading = false;

          if (error.status == 400) {
            this.lecturerForm.reset();
          }
        }
      });
  }

  private fetchGroups(): void {
    this.groupLoading = true;
    this.groupService.getAllGroups().subscribe({
      next: (groupsResponse: GroupsResponse) => {
        this.groups = groupsResponse.groups;
        this.initializeForms();
        this.groupLoading = false;
      },
      error: (error) => {
        this.alertService.error(error.error.title);
        this.groupLoading = false;
      }
    });
  }

  private initializeForms() {
    this.studentForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address: ['', Validators.required],
      birthday: [null, Validators.required],
      groupName: ['', Validators.required]
    });

    this.lecturerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      degree: [''],
      birthday: [null, Validators.required],
      address: ['', Validators.required]
    });
  }
}
