import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TaskService} from "../../../core/services/task.service";
import {AlertService} from "../../../core/services/alert.service";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  fetchLoading: boolean = false;
  taskId: string = '';

  constructor(private taskService: TaskService,
              private alertService: AlertService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.taskId = params['id'];
    });

    if (this.taskId) {
      this.fetchTaskData();
    }
  }

  private fetchTaskData() {
    this.fetchLoading = true;
  //   this.taskService.getTask(this.taskId)
  //     .subscribe({
  //       next: task => {
  //         this.task = task;
  //         this.fetchLoading = false;
  //       },
  //       error: err => {
  //         this.alertService.error(err);
  //         this.fetchLoading = false;
  //       }
  //     });
  }
}
