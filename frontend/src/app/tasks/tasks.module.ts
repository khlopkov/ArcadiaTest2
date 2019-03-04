import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SingleTaskComponent } from './tasks.singleTask';
import { TasksComponent } from './tasks.component';
import { TasksService } from 'src/services/http/tasks.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [ SingleTaskComponent, TasksComponent ],
  imports: [
    BrowserModule,
    HttpClientModule,
  ],
  providers: [ TasksService, HttpClient ],
  exports: [ TasksComponent ],
  bootstrap: [ TasksComponent ]
})
export class TaskModule { }

