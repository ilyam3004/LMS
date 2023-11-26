import {Component} from '@angular/core';
import {StudentTaskStatus, LecturerTask} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {ActivatedRoute} from "@angular/router";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {
  ConfirmationModalComponent
} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GradeEntryModalComponent} from "../../../lecturer/components/grade-entry-modal/grade-entry-modal.component";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent {
  fetchLoading: boolean = false;
  task: LecturerTask = {} as LecturerTask;
  taskId: string = '';

  constructor(private taskService: TaskService,
              private alertService: AlertService,
              private route: ActivatedRoute,
              protected modalService: NgbModal,
              protected dateTimeService: DateTimeService) {
  }

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
          console.log(task)
          this.fetchLoading = false;
        },
        error: err => {
          this.alertService.error(err);
          this.fetchLoading = false;
        }
      });
  }

  getTaskStatus(status: StudentTaskStatus): string {
    return status === StudentTaskStatus.Accepted ? 'Accepted' :
      status === StudentTaskStatus.Returned ? 'Returned' :
        status === StudentTaskStatus.Uploaded ? 'Turned in' : 'Not uploaded';
  }

  openReturnTaskModal(studentTaskId: string): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = 'Are you sure you want to upload this task?';
    modalRef.componentInstance.title = 'Upload task';

    modalRef.result.then(
      (result) => {
        if (result) {
          this.uploadTask();
        }
      },
      () => {
      }
    );
  }

  uploadTask() {

  }
}