import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import {SubjectComponent} from "./subject/subject.component";
import {TaskComponent} from "./task/task.component";

const routes: Routes = [
  {
    path: '', component:  LayoutComponent,
    children: [
      { path: 'subjects', component: SubjectComponent },
      { path: 'tasks', component: TaskComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturerRoutingModule { }
