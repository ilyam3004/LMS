import {Component, OnInit, TemplateRef} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreateSubjectRequest, LecturerSubject} from "../../../../core/models/subject";
import {Group} from "../../../../core/models/group";
import {SubjectService} from "../../../../core/services/subject.service";
import {GroupService} from "../../../../core/services/group.service";
import {AlertService} from "../../../../core/services/alert.service";

@Component({
  selector: 'app-subject',
  templateUrl: './subjects.component.html',
  styleUrl: './subjects.component.scss'
})
export class SubjectsComponent implements OnInit {
  createSubjectForm!: FormGroup;
  subjects: LecturerSubject[] = [];
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

  get createSubjectFormControl() {
    return this.createSubjectForm.controls;
  }

  fetchSubjects(): void {
    this.fetchLoading = true;
    this.subjectService.getLecturerSubjects()
      .subscribe(
        {
          next: subjects => {
            this.subjects = subjects;
            console.log(this.subjects)
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

    this.alertService.clear();
    if (this.createSubjectForm.invalid) {
      return;
    }
    this.createLoading = true;

    const request: CreateSubjectRequest = this.createSubjectForm.value;

    this.subjectService.createSubject(request)
      .subscribe({
        next: (subjectsResponse) => {
          this.alertService.success('Subject created successfully',
            {keepAfterRouteChange: true});
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
    this.createSubjectForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(400)]],
      groupName: ['', Validators.required],
    });
  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }
}