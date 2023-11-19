import {Component, inject, OnInit, TemplateRef} from '@angular/core';
import {SubjectService} from "../../../core/services/subject.service";
import {AlertService} from "../../../core/services/alert.service";
import {Subject} from "../../../core/models/subject";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.scss'
})
export class SubjectComponent implements OnInit {
  subjects: Subject[] = [];
  loading: boolean = false;

  constructor(private subjectService: SubjectService,
              private alertService: AlertService,
              private modalService: NgbModal) {
  }

  ngOnInit() {
    this.fetchSubjects();
  }

  fetchSubjects(): void {
    this.loading = true;
    this.subjectService.getLecturerSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
            this.loading = false;
          },
          error: err => {
            this.alertService.error(err);
            this.loading = false;
          }
        });
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }
}
