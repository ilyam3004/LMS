import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {StudentTaskStatus, StudentTask, UploadedStudentTask} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {ActivatedRoute} from "@angular/router";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
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
          this.alertService.error(this.getErrorMessage(err));
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
          this.alertService.error(this.getErrorMessage(err));
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
          this.alertService.error(this.getErrorMessage(err));
          this.createTaskLoading = false;
        }
      })
  }

  onDownload(studentTaskId: string): void {
    this.taskService.downloadSolution(studentTaskId).subscribe({
        next: (response) => {
          const contentDispositionHeader = response.headers.get('Content-Disposition');

          if (contentDispositionHeader) {
            const matches = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/.exec(contentDispositionHeader);
            const fileName = matches && matches.length > 1 ? matches[1] : null;

            if (fileName) {
              const data = response.body as Blob;

              const a = document.createElement('a');
              a.download = fileName;
              a.href = URL.createObjectURL(data);
              a.click();
            } else {
              this.alertService.error('Unable to extract filename from Content-Disposition header.');
              this.downloadFileWithDefaultName(response);
            }
          } else {
            this.alertService.error('Content-Disposition header not found in the response.' +
              'Downloading file with default filename.');
            this.downloadFileWithDefaultName(response);
          }
        },
        error:
          err => {
            this.alertService.error(err);
          }
      }
    );
  }

  downloadFileWithDefaultName(response: any): void {
    const data = response.body as Blob;

    const a = document.createElement('a');
    a.download = 'file';
    a.href = URL.createObjectURL(data);
    a.click();
  }

  private sortTaskCommentsByDate() {
    this.task.uploadedTask.comments.sort((a, b) => {
      return new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime();
    });
  }

  protected getDeadlineColor() {
    return (
      (this.task.uploadedTask.status === StudentTaskStatus.NotUploaded ||
        this.task.uploadedTask.status === StudentTaskStatus.Returned ||
        this.task.uploadedTask.status === StudentTaskStatus.Rejected) &&
      !this.dateTimeService.isDateInFuture(this.task.deadline)
    ) ? "red" : undefined;
  }

  getErrorMessage(error: any): string {
    return error.status == 0
      ? "Server is not responding. Please try again later"
      : error.error.title;
  }

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
