import {StudentSubject} from "../../../../core/models/subject";
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import {Component, OnInit} from '@angular/core';
import {StudentTaskStatus} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {DateTimeService} from "../../../../core/services/datetime.service";

@Component({
  selector: 'student-grades',
  templateUrl: './grades.component.html',
  styleUrl: './grades.component.scss'
})
export class GradesComponent implements OnInit {
  subjects: StudentSubject[] = [];
  fetchLoading: boolean = false;

  constructor(private subjectService: SubjectService,
              private alertService: AlertService,
              protected taskService: TaskService,
              protected dateTimeService: DateTimeService) {
  }

  ngOnInit() {
    this.fetchSubjects();
  }

  fetchSubjects(): void {
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
            this.alertService.error(err);
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
