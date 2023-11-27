import {Component, OnInit} from '@angular/core';
import {StudentSubject} from "../../../../core/models/subject";
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {TaskService} from "../../../../core/services/task.service";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent implements OnInit {
  subjects: StudentSubject[] = [];

  fetchLoading: boolean = false;
  submitted: boolean = false;

  constructor(private subjectService: SubjectService,
              protected taskService: TaskService,
              private alertService: AlertService,
              protected dateTimeService: DateTimeService) { }
  ngOnInit() {
    this.fetchStudentSubjectsWithTasks();
  }

  fetchStudentSubjectsWithTasks(): void {
    this.alertService.clear();
    this.fetchLoading = true;
    this.subjectService.getStudentSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
            this.fetchLoading = false;
          },
          error: err => {
            this.alertService.error(err);
            this.fetchLoading = false;
          }
        });
  }

  protected readonly TaskService = TaskService;
}
