import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';
import { TaskModule } from './tasks/tasks.module';
import { DashboardModule } from './dashboards/dashboard.module';
import { DashboardComponent } from './dashboards/dashboard.component';

const routes: Routes = [
  { path: '', component: TasksComponent },
  { path: 'dashboard', component: DashboardComponent}
];

@NgModule({
  imports: [TaskModule, DashboardModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule { }
