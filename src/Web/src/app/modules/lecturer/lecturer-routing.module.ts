import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LecturerDashboardComponent } from './home/lecturer-dashboard.component';

const routes: Routes = [
  {
    path: 'home', component: LecturerDashboardComponent,
    children: [
      //{ path: '', component:  },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturerRoutingModule { }
