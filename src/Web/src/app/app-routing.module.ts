import {LayoutComponent} from "./modules/lecturer/layout/layout.component";
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from "./helpers/auth.guard";
import {StudentDashboardComponent} from "./modules/student/student-dashboard/student-dashboard.component";

const authModule = () => import('./modules/auth/auth.module').then(x => x.AuthModule);
const lecturerModule = () => import('./modules/lecturer/lecturer.module').then(x => x.LecturerModule);
const studentModule = () => import('./modules/student/student.module').then(x => x.StudentModule);

const routes: Routes = [
  {path: 'account', loadChildren: authModule},
  {path: 'lecturer', loadChildren: lecturerModule, canActivate: [AuthGuard], data: {expectedRole: 'Lecturer'}},
  {path: 'student', loadChildren: studentModule, canActivate: [AuthGuard], data: {expectedRole: 'Student'}},
  {path: '**', redirectTo: 'account/login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
