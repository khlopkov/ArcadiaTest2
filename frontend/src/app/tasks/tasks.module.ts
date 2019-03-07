import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SingleTaskComponent } from './tasks.singleTask';
import { TasksComponent } from './tasks.component';
import { TasksService } from 'src/services/http/tasks.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RepresentationModule } from '../representation/representation.module';
import { CreateTaskFormComponent } from './tasks.createForm';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [ SingleTaskComponent, TasksComponent, CreateTaskFormComponent ],
  imports: [
    BrowserModule,
    RepresentationModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [ TasksService, HttpClient ],
  exports: [ TasksComponent ],
  bootstrap: [ TasksComponent ]
})
export class TaskModule { }

