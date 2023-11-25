import {Component, inject, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TaskService} from "../../../core/services/task.service";
import {AlertService} from "../../../core/services/alert.service";
import {LecturerTask, StudentTaskStatus} from "../../../core/models/task";
import {DateTimeService} from "../../../core/services/datetime.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ConfirmationModalComponent} from "../../../shared/components/confirmation-modal/confirmation-modal.component";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  removeLoading: boolean = false;
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
    this.taskService.getTask(this.taskId)
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

  removeTask() {

  }

  getTurnedInCount(): number {
    return this.task.studentTasks.filter(task =>
      task.status === StudentTaskStatus.Uploaded).length;
  }

  getAcceptedCount(): number {
    return this.task.studentTasks.filter(task =>
      task.status === StudentTaskStatus.Accepted).length;
  }

  getReturnedCount() {
    return this.task.studentTasks.filter(task =>
      task.status === StudentTaskStatus.Rejected).length;
  }

  getStudentTaskStatus(status: StudentTaskStatus): string {
    return status === StudentTaskStatus.Accepted ? 'Accepted' :
      status === StudentTaskStatus.Rejected ? 'Returned' :
        status === StudentTaskStatus.Uploaded ? 'Turned in' : 'Not uploaded';
  }

  openConfirmationModal(): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = 'Are you sure you want to return this task to the student?';
    modalRef.componentInstance.title = 'Return task';

    modalRef.result.then(
      (result) => {
        if (result) {
          this.returnTaskToStudent();
        }
      },
      () => { }
    );
  }

  private returnTaskToStudent() {
    this.taskService.returnTaskToStudent(this.taskId)
      .subscribe({
        next: task => {
          this.task = task;
          this.alertService.success('Task returned successfully', {keepAfterRouteChange: true});
        },
        error: err => {
          this.alertService.error(err);
        }
      });
  }
}
