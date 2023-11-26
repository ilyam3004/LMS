import {Component, OnInit} from '@angular/core';
import {SubjectService} from "../../../../core/services/subject.service";
import {AlertService} from "../../../../core/services/alert.service";
import { StudentSubject } from '../../../../core/models/subject';

@Component({
  selector: 'app-subject',
  templateUrl: './subjects.component.html',
  styleUrl: './subjects.component.scss'
})
export class SubjectsComponent implements OnInit {
  subjects: StudentSubject[] = [];
  fetchLoading: boolean = false;

  constructor(private subjectService: SubjectService,
              private alertService: AlertService) { }

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
}
