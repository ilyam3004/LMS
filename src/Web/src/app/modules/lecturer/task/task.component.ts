import {Component, OnInit} from '@angular/core';
import {TaskService} from "../../../core/services/task.service";
import {Task} from "../../../core/models/task";
import {Subject} from "../../../core/models/subject";
import {AlertService} from "../../../core/services/alert.service";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.scss'
})
export class TaskComponent implements OnInit {
  fetchLoading: boolean = false;
  tasks: Task[] = [];
  subjects: Subject[] = [];

  constructor(private taskService: TaskService,
              private alertService: AlertService) {
  }

  ngOnInit() {
    this.fetchTasks();
  }

  fetchTasks(): void {
    this.fetchLoading = true;
    this.taskService.getLecturerTasks()
      .subscribe(
        {
          next: subjects => {
          },
          error: err => {
            this.alertService.error(err);
            this.fetchLoading = false;
          }
        });
  }
}
