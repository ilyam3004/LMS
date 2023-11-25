import { NgModule } from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';

import { LecturerRoutingModule } from './lecturer-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SubjectComponent } from './subject/subject.component';
import { TaskComponent } from './task/task.component';
import {
  NgbAccordionModule, NgbActiveModal,
  NgbDropdownModule,
  NgbInputDatepicker,
  NgbTimepicker
} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TaskDetailsComponent } from './task-details/task-details.component';
import { GradeEntryModalComponent } from './grade-entry-modal/grade-entry-modal.component';


@NgModule({
  declarations: [
    LayoutComponent,
    NavbarComponent,
    SubjectComponent,
    TaskComponent,
    TaskDetailsComponent,
    GradeEntryModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LecturerRoutingModule,
    NgbAccordionModule,
    NgbInputDatepicker,
    ReactiveFormsModule,
    NgbTimepicker,
    NgbDropdownModule
  ],
  providers: [NgbActiveModal]
})
export class LecturerModule { }
