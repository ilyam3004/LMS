import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthGuard} from "./helpers/auth.guard";
import {HomeComponent} from "./modules/lecturer/home/home.component";

const authModule = () => import('./modules/auth/auth.module').then(x => x.AuthModule);

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'account', loadChildren: authModule },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
