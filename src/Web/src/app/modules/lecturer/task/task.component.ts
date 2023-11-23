import {AlertService} from "../../../core/services/alert.service";
import {TaskService} from "../../../core/services/task.service";
import {Subject} from "../../../core/models/subject";
import {Component, OnInit} from '@angular/core';
import {SubjectService} from "../../../core/services/subject.service";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.scss'
})
export class TaskComponent implements OnInit {
  fetchLoading: boolean = false;
  subjects: Subject[] = [];
  removeLoading: boolean = false;

  constructor(private taskService: TaskService,
              private subjectService: SubjectService,
              private alertService: AlertService,
              private datePipe: DatePipe) {
  }

  ngOnInit() {
    this.fetchSubjectsWithTasks();
  }

  fetchSubjectsWithTasks(): void {
    this.fetchLoading = true;
    this.subjectService.getLecturerSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
            console.log(this.subjects);
            this.fetchLoading = false;
          },
          error: err => {
            this.alertService.error(err);
            this.fetchLoading = false;
          }
        });
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
}
