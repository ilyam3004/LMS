import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {LecturerTask, StudentTaskStatus} from "../../../../core/models/task";
import {TaskService} from "../../../../core/services/task.service";
import {AlertService} from "../../../../core/services/alert.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {
  ConfirmationModalComponent
} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";
import {GradeEntryModalComponent} from "../../components/grade-entry-modal/grade-entry-modal.component";
import {HttpResponse} from "@angular/common/http";

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
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
    this.taskService.downloadFile(studentTaskId).subscribe(async (event) => {
      let data = event as HttpResponse < Blob > ;
      const downloadedFile = new Blob([data.body as BlobPart], {
        type: data.body?.type
      });
      console.log("ddd", downloadedFile)
      if (downloadedFile.type != "") {
        const a = document.createElement('a');
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        a.download = "task.pdf";
        a.href = URL.createObjectURL(downloadedFile);
        a.target = '_blank';
        a.click();
        document.body.removeChild(a);
      }
    });
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

  getStudentTaskStatus(status: StudentTaskStatus): string {
    return status === StudentTaskStatus.Accepted ? 'Accepted' :
      status === StudentTaskStatus.Returned ? 'Returned' :
        status === StudentTaskStatus.Uploaded ? 'Turned in' : 'Not uploaded';
  }

  openReturnTaskModal(studentTaskId: string): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = 'Are you sure you want to return this task to the student?';
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

  protected readonly StudentTaskStatus = StudentTaskStatus;
}
