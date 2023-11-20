import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturerRoutingModule } from './lecturer-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SubjectComponent } from './subject/subject.component';
import { TaskComponent } from './task/task.component';
import {NgbAccordionDirective, NgbAccordionModule, NgbDatepicker, NgbInputDatepicker} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AuthRoutingModule} from "../auth/auth-routing.module";


@NgModule({
  declarations: [
    LayoutComponent,
    NavbarComponent,
    SubjectComponent,
    TaskComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LecturerRoutingModule,
    NgbAccordionModule,
    NgbInputDatepicker,
    ReactiveFormsModule
  ]
})
export class LecturerModule { }
