import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import {SubjectComponent} from "./subject/subject.component";
import {TaskComponent} from "./task/task.component";
import {TaskDetailsComponent} from "./task-details/task-details.component";

const routes: Routes = [
  {
    path: '', component:  LayoutComponent,
    children: [
      { path: 'subjects', component: SubjectComponent },
      { path: 'tasks', component: TaskComponent },
      { path: 'task/:id', component: TaskDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturerRoutingModule { }
