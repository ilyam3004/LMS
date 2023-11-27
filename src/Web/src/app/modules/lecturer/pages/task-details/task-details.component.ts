import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {LecturerTask, StudentTask, StudentTaskStatus, UploadedStudentTask} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {
  ConfirmationModalComponent
} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GradeEntryModalComponent} from "../../components/grade-entry-modal/grade-entry-modal.component";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  fetchLoading: boolean = false;
  task: LecturerTask = {} as LecturerTask;
  taskId: string = '';

  constructor(private alertService: AlertService,
              private route: ActivatedRoute,
              protected taskService: TaskService,
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
    this.taskService.getLecturerTaskDetails(this.taskId)
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
      task.status === StudentTaskStatus.Returned).length;
  }

  openReturnTaskModal(studentTaskId: string): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = 'Are you sure you want to return this task to the student?';
    modalRef.componentInstance.removeOption = true;
    modalRef.componentInstance.title = 'Return task';

    modalRef.result.then(
      (result) => {
        if (result) {
          this.returnTaskToStudent(studentTaskId);
        }
      },
      () => {
      }
    );
  }

  openRejectTaskConfirmationModal(studentTaskId: string): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.title = 'Return task';
    modalRef.componentInstance.message = 'Are you sure you want to reject this task?';
    modalRef.componentInstance.removeOption = true;
    modalRef.componentInstance.warningMessage = 'Note that this action cannot be undone ' +
      'and after rejecting the task student will not be able to upload it again.';

    modalRef.result.then(
      (result) => {
        if (result) {
          this.rejectStudentTask(studentTaskId);
        }
      },
      () => {
      }
    );
  }

  openGradeEntryModal(studentTaskId: string): void {
    const modalRef = this.modalService.open(GradeEntryModalComponent);
    modalRef.componentInstance.maxGrade = this.task.maxGrade;

    modalRef.result.then(
      (result) => {
        if (result) {
          this.acceptStudentTask(result, studentTaskId);
        }
      },
      () => {
      }
    );
  }

  private returnTaskToStudent(studentTaskId: string) {
    this.taskService.returnTaskToStudent(studentTaskId)
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

  private rejectStudentTask(studentTaskId: string) {
    this.taskService.rejectTask(studentTaskId)
      .subscribe({
        next: task => {
          this.task = task;
          this.alertService.success('Task rejected successfully', {keepAfterRouteChange: true});
        },
        error: err => {
          this.alertService.error(err);
        }
      });
  }

  private acceptStudentTask(grade: number, studentTaskId: string) {
    this.taskService.acceptTask(studentTaskId, grade)
      .subscribe({
        next: task => {
          this.task = task;
          this.alertService.success('Task accepted successfully', {keepAfterRouteChange: true});
        },
        error: err => {
          this.alertService.error(err);
        }
      })
  }

  protected getUploadedDateColor(uploadedAt: Date | null, deadline: Date): string {
    return uploadedAt! > deadline ? 'red' : 'black';
  }

  canReject(task: LecturerTask, studentTask: UploadedStudentTask): boolean {
    return studentTask.status === StudentTaskStatus.NotUploaded
      && task.deadline < this.dateTimeService.getCurrentDateTime()
      || studentTask.status === StudentTaskStatus.Returned
      && task.deadline < this.dateTimeService.getCurrentDateTime();
  }

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
