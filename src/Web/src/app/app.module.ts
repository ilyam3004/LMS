import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {JwtInterceptor} from "./helpers/jwt.interceptor";
import {ErrorInterceptor} from "./helpers/error.interceptor";
import {ReactiveFormsModule} from "@angular/forms";
import { HomeComponent } from './modules/lecturer/home/home.component';
import { AlertComponent } from './shared/components/alert/alert.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
