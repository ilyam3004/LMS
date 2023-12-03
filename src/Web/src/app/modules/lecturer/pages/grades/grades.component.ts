import {Component, OnInit} from '@angular/core';
import {SubjectGrades} from "../../../../core/models/subject";
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import {TaskService} from "../../../../core/services/task.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import { StudentTaskStatus } from '../../../../core/models/task';

@Component({
  selector: 'app-grades',
  templateUrl: './grades.component.html',
  styleUrl: './grades.component.scss'
})
export class GradesComponent implements OnInit {
  subjectsGrades: SubjectGrades[] = [];
  fetchLoading: boolean = false;

  constructor(private subjectService: SubjectService,
              private alertService: AlertService,
              protected taskService: TaskService,
              protected dateTimeService: DateTimeService) { }

  ngOnInit() {
    this.fetchSubjectsGrades();
  }

  fetchSubjectsGrades(): void {
    this.fetchLoading = true;
    this.subjectService.getLecturerGrades()
      .subscribe(
        {
          next: subjects => {
            this.subjectsGrades = subjects;
            this.fetchLoading = false;
          },
          error: err => {
            this.alertService.error(err.error.title);
            this.fetchLoading = false;
          }
        });
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
            this.alertService.error(err.error.title);
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

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
