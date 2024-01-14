import {Component, OnInit, TemplateRef} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreateSubjectRequest, LecturerSubject, LecturerSubjectsResponse} from "../../../../core/models/subject";
import {Group, GroupsResponse} from "../../../../core/models/group";
import {SubjectService} from "../../../../core/services/subject.service";
import {GroupService} from "../../../../core/services/group.service";
import {AlertService} from "../../../../core/services/alert.service";
import { ConfirmationModalComponent} from "../../../../shared/components/confirmation-modal/confirmation-modal.component";

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
          next: response => {
            this.subjects = response.subjects;
          },
          error: err => {
            this.alertService.error(err.error.message);
            this.fetchLoading = false;
          }
        });
  }

  private fetchGroups(): void {
    this.groupService.getAllGroups().subscribe({
      next: (groupsResponse: GroupsResponse) => {
        this.groups = groupsResponse.groups;
        this.initializeForms();
        this.fetchLoading = false;
      },
      error: (error) => {
        this.alertService.error(error.error.message)
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
        next: (subjectsResponse: LecturerSubjectsResponse) => {
          this.alertService.success('Subject created successfully',
            {keepAfterRouteChange: true});
          this.subjects = subjectsResponse.subjects;
          this.createLoading = false;
          modal.close();
        },
        error: error => {
          this.alertService.error(error.error.message);
          this.createLoading = false;
        }
      });
  }

  openRemoveSubjectConfirmationModal(subjectId: string, subjectName: string): void {
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = `Are you sure you want to remove subject ${subjectName}?`;
    modalRef.componentInstance.isWarning = true;
    modalRef.componentInstance.title = 'Remove subject';

    modalRef.result.then(
      (result) => {
        if (result) {
          this.removeSubject(subjectId);
        }
      },
      () => {
      }
    );
  }

  removeSubject(subjectId: string): void {
    this.alertService.clear();
    this.removeLoading = true;

    this.subjectService.removeSubject(subjectId)
      .subscribe({
        next: (subjectResponse: LecturerSubjectsResponse) => {
          this.subjects = subjectResponse.subjects;
          this.alertService.success("Subject removed successfully");
          this.removeLoading = false;
        },
        error: err => {
          this.alertService.error(err.error.title);
          this.removeLoading = false;
        }
      });
  }

  private initializeForms() {
    this.createSubjectForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
      groupName: ['', Validators.required],
    });

  }

  openModal(content: TemplateRef<any>) {
    this.modalService.open(content, {size: 'lg'});
  }
}
