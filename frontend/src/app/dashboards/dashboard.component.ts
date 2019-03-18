import {Component, OnInit} from '@angular/core';

import { BarDataModel } from './charts/models/bar.model';
import { StatisticsService } from 'src/services/http/statistics.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  constructor(private statistitcsService: StatisticsService) { }

  barChartData: BarDataModel[] = [];

  ngOnInit(): void {
    this.fetchDashboard();
  }

  dashboardToBarData(dashboard: { [ key: string ]: number }): BarDataModel[] {
    return Object.keys(dashboard).map(key => new BarDataModel(key, dashboard[key]));
  }

  fetchDashboard() {
    this.statistitcsService.statisticsTasksCountByStatus().subscribe(data => {
      this.barChartData = this.dashboardToBarData(data);
    });
  }
}
