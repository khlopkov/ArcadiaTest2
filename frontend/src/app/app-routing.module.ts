import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';
import { TaskModule } from './tasks/tasks.module';
import { DashboardModule } from './dashboards/dashboard.module';
import { DashboardComponent } from './dashboards/dashboard.component';
import { AuthGuard } from 'src/services/auth.guard';
import { LoginComponent } from './login/login.component';
import { LoginModule } from './login/login.module';
import { HistoryModule } from './history/hitsory.module';
import { HistoryComponent } from './history/history.component';

const routes: Routes = [
  { path: '', component: TasksComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'history', component: HistoryComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [LoginModule, TaskModule, DashboardModule, HistoryModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard],
  declarations: [],
})
export class AppRoutingModule { }
