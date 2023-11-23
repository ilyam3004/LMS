import {Component, inject, OnInit, TemplateRef} from '@angular/core';
import {SubjectService} from "../../../core/services/subject.service";
import {AlertService} from "../../../core/services/alert.service";
import {CreateSubjectRequest, Subject} from "../../../core/models/subject";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Group} from "../../../core/models/group";
import {GroupService} from "../../../core/services/group.service";

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.scss'
})
export class SubjectComponent implements OnInit {
  requestForm!: FormGroup;
  subjects: Subject[] = [];
  groups: Group[] = [];
  fetchLoading: boolean = false;
  createLoading: boolean = false;
  removeLoading: boolean = false;
  submitted: boolean = false;

  constructor(private subjectService: SubjectService,
              private groupService: GroupService,
              private alertService: AlertService,
              private modalService: NgbModal,
              private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.fetchSubjects();
    this.fetchGroups();
  }

  get requestFormControl() {
    return this.requestForm.controls;
  }

  fetchSubjects(): void {
    this.fetchLoading = true;
    this.subjectService.getLecturerSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
          },
          error: err => {
            this.alertService.error(err);
            this.fetchLoading = false;
          }
        });
  }

  private fetchGroups(): void {
    this.groupService.getAllGroups().subscribe({
      next: (groupsResponse: Group[]) => {
        this.groups = groupsResponse;
        this.initializeForms();
        this.fetchLoading = false;
      },
      error: (error) => {
        this.alertService.error(error);
        this.fetchLoading = false;
      }
    });
  }

  onCreateSubjectFormSubmit(modal: any) {
    this.submitted = true;

    console.log(this.requestForm.errors)
    this.alertService.clear();
    if (this.requestForm.invalid) {
      return;
    }
    console.log(this.requestForm.value)
    this.createLoading = true;

    const request: CreateSubjectRequest = this.requestForm.value;

    this.subjectService.createSubject(request)
      .subscribe({
        next: (subjectsResponse) => {
          this.alertService.success('Subject created successfully', {keepAfterRouteChange: true});
          this.subjects = subjectsResponse;
          this.createLoading = false;
          modal.close();
        },
        error: error => {
          this.alertService.error(error);
          this.createLoading = false;
        }
      });
  }

  removeSubject(subjectId: string): void {
    this.alertService.clear();
    this.removeLoading = true;

    this.subjectService.removeSubject(subjectId)
      .subscribe({
        next: subjectResponse => {
          this.subjects = subjectResponse
          this.alertService.success("Subject removed successfully");
          this.removeLoading = false;
        },
        error: err => {
          this.alertService.error(err);
          this.removeLoading = false;
        }
      });
  }

  private initializeForms() {
    this.requestForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(400)]],
      groupName: ['', Validators.required],
    });
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }
}
