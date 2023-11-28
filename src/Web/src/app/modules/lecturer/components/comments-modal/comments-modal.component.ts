import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {TaskService} from "../../../../core/services/task.service";
import {UploadedStudentTask} from "../../../../core/models/task";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AlertService} from "../../../../core/services/alert.service";
import {AuthenticationService} from "../../../../core/services/authentication.service";

@Component({
  selector: 'app-comments-modal',
  templateUrl: './comments-modal.component.html',
  styleUrl: './comments-modal.component.scss'
})
export class CommentsModalComponent implements OnInit {
  @ViewChild('commentContainer') commentContainer?: ElementRef;
  @Output() addCommentEvent = new EventEmitter<UploadedStudentTask>();
  @Input() uploadedTask: UploadedStudentTask = {} as UploadedStudentTask;

  createTaskLoading: boolean = false;
  createTaskCommentForm!: FormGroup;
  submitted: boolean = false;

  constructor(protected activeModal: NgbActiveModal,
              private taskService: TaskService,
              private formBuilder: FormBuilder,
              private alertService: AlertService,
              protected authenticationService: AuthenticationService,
              protected dateTimeService: DateTimeService) { }

  ngOnInit(): void {
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

    this.taskService.commentTask(this.uploadedTask.studentTaskId, comment)
      .subscribe({
        next: response => {
          this.addCommentEvent.emit(response);
          this.uploadedTask = response;
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
}
