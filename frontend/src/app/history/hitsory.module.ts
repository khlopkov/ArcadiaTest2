import { HistoryComponent } from './history.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HistoryService } from 'src/services/http/history.service';

@NgModule({
  declarations: [
    HistoryComponent
  ],
  imports: [
    BrowserModule,
  ],
  providers: [
    HistoryService,
  ],
  bootstrap: [HistoryComponent]
})
export class HistoryModule { }
