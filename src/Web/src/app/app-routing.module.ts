import {LecturerDashboardComponent} from "./modules/lecturer/home/lecturer-dashboard.component";
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthGuard} from "./helpers/auth.guard";
import {StudentDashboardComponent} from "./modules/student/student-dashboard/student-dashboard.component";

const authModule = () => import('./modules/auth/auth.module').then(x => x.AuthModule);

const routes: Routes = [
  { path: 'lecturer', component: LecturerDashboardComponent, canActivate: [AuthGuard], data: { expectedRole: 'Lecturer' } },
  { path: 'student', component: StudentDashboardComponent, canActivate: [AuthGuard], data: { expectedRole: 'Student' } },
  { path: 'account', loadChildren: authModule },
  { path: '**', redirectTo: 'account/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
