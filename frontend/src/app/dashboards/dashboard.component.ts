import {Component, OnInit} from '@angular/core';

import { BarDataModel } from './charts/models/bar.model';
import { StatisticsService, TasksCountByStatusDictionary } from 'src/services/http/statistics.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  constructor(private statisticsService: StatisticsService) { }

  barChartData: BarDataModel[] = [];

  ngOnInit(): void {
    this.fetchDashboard();
  }

  dashboardToBarData(dashboard: TasksCountByStatusDictionary): BarDataModel[] {
    return Object.keys(dashboard).map(key => new BarDataModel(key, dashboard[key]));
  }

  fetchDashboard() {
    this.statisticsService.getStatisticsTasksCountByStatus().subscribe(data => {
      this.barChartData = this.dashboardToBarData(data);
    });
  }
}
