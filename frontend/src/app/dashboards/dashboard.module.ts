import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { DashboardComponent } from './dashboard.component';
import { BarChartModule } from './charts/barChart/barChart.module';
import { TasksService } from 'src/services/http/tasks.service';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BarChartModule,
  ],
  providers: [TasksService],
  bootstrap: [DashboardComponent],
  exports: [DashboardComponent],
})
export class DashboardModule { }
