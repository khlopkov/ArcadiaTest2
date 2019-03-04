import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SingleTaskComponent } from './tasks.singleTask';


@NgModule({
  declarations: [ SingleTaskComponent ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  exports: [ SingleTaskComponent ],
  bootstrap: [ SingleTaskComponent ]
})
export class TaskModule { }

