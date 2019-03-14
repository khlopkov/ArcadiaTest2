import { HistoryComponent } from './history.component';
import { BrowserModule } from '@angular/platform-browser';
import { TasksService } from 'src/services/http/tasks.service';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [
    HistoryComponent
  ],
  imports: [
    BrowserModule,
  ],
  providers: [
    TasksService,
  ],
  bootstrap: [HistoryComponent]
})
export class HistoryModule { }
