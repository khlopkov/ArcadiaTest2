import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { DashboardComponent } from './dashboard.component';
import { BarChartModule } from './charts/barChart/barChart.module';
import { StatisticsService } from 'src/services/http/statistics.service';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BarChartModule,
  ],
  providers: [StatisticsService],
  bootstrap: [DashboardComponent],
  exports: [DashboardComponent],
})
export class DashboardModule { }
