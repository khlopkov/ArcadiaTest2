import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { BarChartComponent } from './barChart.component';

@NgModule({
  declarations: [
    BarChartComponent
  ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  exports: [BarChartComponent],
  bootstrap: [BarChartComponent]
})
export class BarChartModule { }
