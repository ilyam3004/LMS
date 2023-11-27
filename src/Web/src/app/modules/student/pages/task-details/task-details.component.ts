import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {StudentTaskStatus, StudentTask} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {ActivatedRoute} from "@angular/router";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {ConfirmationModalComponent} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  @ViewChild('fileUploader') fileUploader: ElementRef = {} as ElementRef;
  fetchLoading: boolean = false;
  task: StudentTask = {} as StudentTask;
  file: File | null = null;

  taskId: string = '';

  constructor(protected taskService: TaskService,
              private alertService: AlertService,
              private route: ActivatedRoute,
              protected modalService: NgbModal,
              protected dateTimeService: DateTimeService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.taskId = params['id'];
    });

    this.fetchTaskData();
  }

  private fetchTaskData() {
    this.fetchLoading = true;
    this.taskService.getStudentTaskDetails(this.taskId)
      .subscribe({
        next: task => {
          this.task = task;
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
      () => { }
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

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
