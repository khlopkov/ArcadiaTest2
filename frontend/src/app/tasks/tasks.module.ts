import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SingleTaskComponent } from './tasks.singleTask';
import { TasksComponent } from './tasks.component';


@NgModule({
  declarations: [ SingleTaskComponent, TasksComponent ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  exports: [ TasksComponent ],
  bootstrap: [ TasksComponent ]
})
export class TaskModule { }

