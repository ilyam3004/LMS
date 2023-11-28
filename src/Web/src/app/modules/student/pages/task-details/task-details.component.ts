import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {StudentTaskStatus, StudentTask, UploadedStudentTask} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {ActivatedRoute} from "@angular/router";
import {NgbActiveModal, NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {
  ConfirmationModalComponent
} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../../core/services/authentication.service";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  @ViewChild('fileUploader') fileUploader: ElementRef = {} as ElementRef;
  task: StudentTask = {} as StudentTask;
  createTaskCommentForm!: FormGroup;
  file: File | null = null;
  taskId: string = '';

  fetchLoading: boolean = false;
  createTaskLoading: boolean = false;
  submitted: boolean = false;

  constructor(protected taskService: TaskService,
              private alertService: AlertService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder,
              protected authenticationService: AuthenticationService,
              protected modalService: NgbModal,
              protected dateTimeService: DateTimeService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.taskId = params['id'];
    });

    this.fetchTaskData();
    this.initializeForm();
  }

  private fetchTaskData() {
    this.fetchLoading = true;
    this.taskService.getStudentTaskDetails(this.taskId)
      .subscribe({
        next: task => {
          this.task = task;
          this.sortTaskCommentsByDate();
          this.fetchLoading = false;
        },
        error: err => {
          this.alertService.error(err);
          this.fetchLoading = false;
        }
      });
  }

  onFileSelected(event: any): void {
    this.file = event.target.files[0];
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = `Are you sure you want to upload ${this.file?.name}?`;
    modalRef.componentInstance.title = 'Upload task';

    modalRef.result.then(
      (result) => {
        if (result && this.file) {
          this.uploadTask();
          this.resetFileInput();
        }
      },
      () => {
      }
    );
  }

  uploadTask(): void {
    this.taskService.uploadSolutions(this.file!, this.task.uploadedTask.studentTaskId)
      .subscribe({
        next: task => {
          this.task = task;
          this.alertService.success('Task solution uploaded successfully!');
        },
        error: err => {
          this.alertService.error(err);
        }
      });
  }

  resetFileInput(): void {
    if (this.fileUploader) {
      this.fileUploader.nativeElement.value = '';
    }
    this.file = null;
  }


  initializeForm() {
    this.createTaskCommentForm = this.formBuilder.group({
      comment: ['', [Validators.required, Validators.maxLength(300)]],
    });
  }

  get createTaskCommentFormControl() {
    return this.createTaskCommentForm.controls;
  }

  onCreateCommentFormSubmit() {
    this.submitted = true;
    this.alertService.clear();

    if (this.createTaskCommentForm.invalid) {
      return;
    }

    this.createTaskLoading = true;

    const comment: string = this.createTaskCommentForm.value.comment;

    this.taskService.commentTask(this.task.uploadedTask.studentTaskId, comment)
      .subscribe({
        next: response => {
          this.task.uploadedTask = response;
          this.sortTaskCommentsByDate();
          this.createTaskLoading = false;
          this.submitted = false;
          this.createTaskCommentForm.get('comment')?.reset();
        },
        error: err => {
          this.alertService.error(err);
          this.createTaskLoading = false;
        }
      })
  }

  private sortTaskCommentsByDate() {
    this.task.uploadedTask.comments.sort((a, b) => {
      return new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime();
    });
  }

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
