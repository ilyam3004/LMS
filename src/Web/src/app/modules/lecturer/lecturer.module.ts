import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturerRoutingModule } from './lecturer-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SubjectComponent } from './subject/subject.component';
import { TaskComponent } from './task/task.component';


@NgModule({
  declarations: [
    LayoutComponent,
    NavbarComponent,
    SubjectComponent,
    TaskComponent
  ],
  imports: [
    CommonModule,
    LecturerRoutingModule
  ]
})
export class LecturerModule { }
