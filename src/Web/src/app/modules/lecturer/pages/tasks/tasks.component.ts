import {Component, OnInit, TemplateRef} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NgbDateStruct, NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {LecturerSubject} from '../../../../core/models/subject';
import {TaskService} from "../../../../core/services/task.service";
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import {DateTimeService} from "../../../../core/services/datetime.service";
import {AssignTaskRequest} from "../../../../core/models/task";

@Component({
  selector: 'app-task',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent implements OnInit {
  assignTaskForm!: FormGroup;
  subjects: LecturerSubject[] = [];
  dateModel: NgbDateStruct | null = null;
  timeModel: { hour: number, minute: number } = {hour: 0, minute: 0};
  isDateSet: boolean = false;

  removeLoading: boolean = false;
  fetchLoading: boolean = false;
  submitted: boolean = false;
  createLoading: boolean = false;

  constructor(private taskService: TaskService,
              private subjectService: SubjectService,
              private alertService: AlertService,
              private modalService: NgbModal,
              private formBuilder: FormBuilder,
              protected dateTimeService: DateTimeService) {
  }

  ngOnInit() {
    this.initializeForms();
    this.fetchSubjectsWithTasks();
  }

  get assignTaskFormControl() {
    return this.assignTaskForm.controls;
  }

  private initializeForms() {
    this.assignTaskForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(2000)]],
      maxGrade: ['', [Validators.required, Validators.min(1), Validators.max(100)]]
    });
  }

  fetchSubjectsWithTasks(): void {
    this.fetchLoading = true;
    this.subjectService.getLecturerSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
            this.fetchLoading = false;
          },
          error: err => {
            if (err.status == 0) {
              this.alertService.error("Server is not responding. Please try again later");
            } else {
              this.alertService.error(err.error.title);
            }
            this.fetchLoading = false;
          }
        });
  }

  onAssignTaskFormSubmit(modal: any, subjectId: string): void {
    this.submitted = true;

    this.alertService.clear();
    if (this.assignTaskForm.invalid) {
      return;
    }


    const request: AssignTaskRequest = {
      title: this.assignTaskForm.value.title,
      description: this.assignTaskForm.value.description,
      subjectId: subjectId,
      maxGrade: this.assignTaskForm.value.maxGrade,
      deadline: null
    };

    if (this.dateModel !== null && this.dateModel.year !== 1) {
      const deadlineDate = new Date(this.dateModel.year,
        this.dateModel.month - 1,
        this.dateModel.day, this.timeModel.hour,
        this.timeModel.minute);

      if (!this.dateTimeService.isDateValid(deadlineDate)) {
        this.alertService.error("Deadline is not valid. Please try again");
        return;
      }

      if (!this.dateTimeService.isDateInFuture(deadlineDate)) {
        this.alertService.error("Deadline must be in future");
        return;
      }

      request.deadline = deadlineDate.toISOString();
    }
    this.createLoading = true;

    this.taskService.assignTask(request)
      .subscribe({
        next: (subject) => {
          this.alertService.success('Task added successfully', {keepAfterRouteChange: true});
          this.updateSubject(subject);
          this.createLoading = false;
          this.assignTaskForm.reset();
          modal.close();
        },
        error: error => {
          this.alertService.error(this.getErrorMessage(error));
          this.createLoading = false;
        }
      });
  }

  onDateChange() {
    this.isDateSet = this.dateModel !== null;
  }

  removeTask(taskId: string): void {
    this.alertService.clear();
    this.removeLoading = true;

    this.taskService.removeTask(taskId)
      .subscribe({
        next: subject => {
          this.updateSubject(subject);
          this.alertService.success("Task removed successfully");
          this.removeLoading = false;
        },
        error: err => {
          this.alertService.error(this.getErrorMessage(err));
          this.removeLoading = false;
        }
      });
  }

  updateSubject(updatedSubject: LecturerSubject): void {
    const index = this.subjects.findIndex(s =>
      s.subjectId === updatedSubject.subjectId);

    if (index !== -1) {
      this.subjects[index] = updatedSubject;
    }
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }

  getErrorMessage(error: any): string {
    return error.status == 0
      ? "Server is not responding. Please try again later"
      : error.error.title;
  }
}
