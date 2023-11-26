import {Component, OnInit} from '@angular/core';
import {StudentSubject} from "../../../../core/models/subject";
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {StudentTaskStatus} from "../../../../core/models/task";

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

  getTaskStatus(status: StudentTaskStatus): string {
    return status === StudentTaskStatus.Accepted ? 'Accepted' :
      status === StudentTaskStatus.Returned ? 'Returned' :
        status === StudentTaskStatus.Uploaded ? 'Turned in' : 'Not uploaded';
  }

  getTaskStatusColor(status: StudentTaskStatus): string {
    switch (status) {
      case StudentTaskStatus.Uploaded:
        return '#ffb96f';
      case StudentTaskStatus.Accepted:
        return '#00d300';
      case StudentTaskStatus.Returned:
        return 'red';
      case StudentTaskStatus.NotUploaded:
        return 'gray';
      default:
        return 'gray';
    }
  }
}
