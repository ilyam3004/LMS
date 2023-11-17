import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturerRoutingModule } from './lecturer-routing.module';
import { LecturerDashboardComponent } from './home/lecturer-dashboard.component';


@NgModule({
  declarations: [
    LecturerDashboardComponent
  ],
  imports: [
    CommonModule,
    LecturerRoutingModule
  ]
})
export class LecturerModule { }
