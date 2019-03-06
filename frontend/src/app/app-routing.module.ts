import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';
import { TaskModule } from './tasks/tasks.module';

const routes: Routes = [
  { path: '', component: TasksComponent }
];

@NgModule({
  imports: [TaskModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule { }
