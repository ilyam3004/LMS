import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TaskService} from "../../../core/services/task.service";
import {AlertService} from "../../../core/services/alert.service";
import {LecturerTask, StudentTaskStatus} from "../../../core/models/task";
import {DateTimeService} from "../../../core/services/datetime.service";

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
}
