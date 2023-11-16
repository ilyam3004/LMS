import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AuthRoutingModule} from './auth-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import {LayoutComponent} from "./layout/layout.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AuthRoutingModule
  ],
  declarations: [
    LayoutComponent,
    LoginComponent,
    RegisterComponent
  ]
})
export class AuthModule {
}
