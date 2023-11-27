import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./pages/layout/layout.component";
import {SubjectsComponent} from "./pages/subject/subjects.component";
import {TasksComponent} from "./pages/tasks/tasks.component";
import {GradesComponent} from "./pages/grades/grades.component";
import {TaskDetailsComponent} from "./pages/task-details/task-details.component";
import {ProfileComponent} from "./pages/profile/profile.component";

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      {path: 'subjects', component: SubjectsComponent},
      {path: 'tasks', component: TasksComponent},
      {path: 'grades', component: GradesComponent},
      {path: 'tasks/task', component: TaskDetailsComponent},
      {path: 'profile', component: ProfileComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule {
}
