import {Component, Input} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-grade-entry-modal',
  templateUrl: './grade-entry-modal.component.html',
  styleUrl: './grade-entry-modal.component.scss'
})
export class GradeEntryModalComponent {
  @Input() maxGrade: number = 100;
  gradeForm: FormGroup;
  submitted: boolean = false;

  constructor(public activeModal: NgbActiveModal, private fb: FormBuilder) {
    this.gradeForm = this.fb.group({
      grade: [null, [Validators.required, Validators.max(this.maxGrade), Validators.min(1)]],
    });
  }

  get gradeControl() {
    return this.gradeForm.controls;
  }

  onGradeSubmit(): void {
    this.submitted = true;

    if (this.gradeForm.invalid) {
      return;
    }
    const submittedGrade = this.gradeForm.value.grade;
    this.activeModal.close(submittedGrade);
  }

  cancel(): void {
    this.activeModal.dismiss();
  }
}
