import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentRoutingModule } from './student-routing.module';
import {LayoutComponent} from "./pages/layout/layout.component";
import {NavbarComponent} from "./components/navbar/navbar.component";
import {SubjectsComponent} from "./pages/subject/subjects.component";
import {GradesComponent} from "./pages/grades/grades.component";
import { TasksComponent } from './pages/tasks/tasks.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgbInputDatepicker, NgbTimepicker} from "@ng-bootstrap/ng-bootstrap";
import { TaskDetailsComponent } from './pages/task-details/task-details.component';


@NgModule({
  declarations: [
    LayoutComponent,
    NavbarComponent,
    SubjectsComponent,
    GradesComponent,
    TasksComponent,
    NavbarComponent,
    TaskDetailsComponent
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    FormsModule,
    NgbInputDatepicker,
    NgbTimepicker,
    ReactiveFormsModule
  ]
})
export class StudentModule { }
