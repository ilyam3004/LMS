import {AlertService} from "../../../core/services/alert.service";
import {TaskService} from "../../../core/services/task.service";
import {Subject} from "../../../core/models/subject";
import {Component, OnInit, TemplateRef} from '@angular/core';
import {SubjectService} from "../../../core/services/subject.service";
import {DatePipe} from "@angular/common";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AssignTaskRequest} from "../../../core/models/task";
import {NgbDateStruct, NgbModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.scss'
})
export class TaskComponent implements OnInit {
  assignTaskForm!: FormGroup;
  subjects: Subject[] = [];
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
              private datePipe: DatePipe) {
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
            this.alertService.error(err);
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

    this.createLoading = true;

    const request: AssignTaskRequest = {
      title: this.assignTaskForm.value.title,
      description: this.assignTaskForm.value.description,
      subjectId: subjectId,
      maxGrade: this.assignTaskForm.value.maxGrade,
      deadline: null
    };

    if (this.dateModel !== null && this.dateModel.year !== 1) {
      request.deadline = new Date(this.dateModel.year,
        this.dateModel.month - 1,
        this.dateModel.day, this.timeModel.hour,
        this.timeModel.minute).toISOString();
    }

    this.taskService.assignTask(request)
      .subscribe({
        next: (subject) => {
          this.alertService.success('Task added successfully', {keepAfterRouteChange: true});
          this.updateSubject(subject);
          this.createLoading = false;
          modal.close();
        },
        error: error => {
          this.alertService.error(error);
          this.createLoading = false;
        }
      });
  }

  onDateChange(){
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
          this.alertService.error(err);
          this.removeLoading = false;
        }
      });
  }

  updateSubject(updatedSubject: Subject): void {
    const index = this.subjects.findIndex(s =>
      s.subjectId === updatedSubject.subjectId);

    if (index !== -1) {
      this.subjects[index] = updatedSubject;
    }
  }

  convertDateToReadableFormat(isoDate: Date): string {
    const date = new Date(isoDate);

    if (this.isCreatedToday(date)) {
      return 'Today at ' + this.datePipe.transform(isoDate, 'HH:mm') ?? '-';
    }

    if (this.isYearsEqual(date)) {
      const dateWithOutYear = this.datePipe.transform(isoDate, 'MMM d, HH:mm');
      return dateWithOutYear == null ? '-' : dateWithOutYear;
    }

    const readableDate = this.datePipe.transform(isoDate, 'MMM d, y, HH:mm');
    return readableDate == null ? '-' : readableDate;
  }

  private isCreatedToday(date: Date): boolean {
    const currentDate = new Date();

    return date.getDate() === currentDate.getDate()
      && date.getMonth() === currentDate.getMonth()
      && date.getFullYear() === currentDate.getFullYear();
  }

  private isYearsEqual(date: Date): boolean {
    const currentDate = new Date();
    return date.getFullYear() == currentDate.getFullYear();
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }
}
