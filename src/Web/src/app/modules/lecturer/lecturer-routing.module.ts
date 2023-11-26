import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "./pages/layout/layout.component";
import {SubjectsComponent} from "./pages/subjects/subjects.component";
import {TasksComponent} from "./pages/tasks/tasks.component";
import {TaskDetailsComponent} from "./pages/task-details/task-details.component";

const routes: Routes = [
  {
    path: '', component:  LayoutComponent,
    children: [
      { path: 'subjects', component: SubjectsComponent },
      { path: 'tasks', component: TasksComponent },
      { path: 'tasks/task', component: TaskDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturerRoutingModule { }
