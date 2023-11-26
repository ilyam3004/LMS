import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import { LecturerRoutingModule } from './lecturer-routing.module';
import {
  NgbAccordionModule, NgbActiveModal,
  NgbDropdownModule,
  NgbInputDatepicker,
  NgbTimepicker
} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {LayoutComponent} from "../lecturer/pages/layout/layout.component";
import {TaskDetailsComponent} from "./pages/task-details/task-details.component";
import {GradeEntryModalComponent} from "./components/grade-entry-modal/grade-entry-modal.component";
import {TasksComponent} from "./pages/tasks/tasks.component";
import {SubjectsComponent} from "./pages/subjects/subjects.component";
import {NavbarComponent} from "./components/navbar/navbar.component";

@NgModule({
  declarations: [
    LayoutComponent,
    NavbarComponent,
    SubjectsComponent,
    TasksComponent,
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
  exports: [
    NavbarComponent
  ],
  providers: [NgbActiveModal]
})
export class LecturerModule { }
