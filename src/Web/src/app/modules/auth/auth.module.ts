import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AuthRoutingModule} from './auth-routing.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {LayoutComponent} from "./layout/layout.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {NgbDatepicker, NgbInputDatepicker} from "@ng-bootstrap/ng-bootstrap";
import {LecturerModule} from "../lecturer/lecturer.module";

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    FormsModule,
    NgbDatepicker,
    NgbInputDatepicker,
    LecturerModule
  ],
  declarations: [
    LayoutComponent,
    LoginComponent,
    RegisterComponent
  ]
})
export class AuthModule {
}
