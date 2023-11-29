import {LayoutComponent} from "./pages/layout/layout.component";
import {SubjectsComponent} from "./pages/subjects/subjects.component";
import {TasksComponent} from "./pages/tasks/tasks.component";
import {TaskDetailsComponent} from "./pages/task-details/task-details.component";
import {ProfileComponent} from "./pages/profile/profile.component";
import {GradesComponent} from "./pages/grades/grades.component";
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '', component:  LayoutComponent,
    children: [
      { path: 'subjects', component: SubjectsComponent },
      { path: 'tasks', component: TasksComponent },
      { path: 'tasks/task', component: TaskDetailsComponent},
      { path: "grades", component: GradesComponent },
      { path: 'profile', component: ProfileComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturerRoutingModule { }
