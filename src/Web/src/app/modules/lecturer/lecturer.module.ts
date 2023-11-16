import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturerRoutingModule } from './lecturer-routing.module';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    LecturerRoutingModule
  ]
})
export class LecturerModule { }