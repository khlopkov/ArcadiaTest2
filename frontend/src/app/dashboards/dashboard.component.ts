import {Component, OnInit} from '@angular/core';

import { BarDataModel } from './charts/models/bar.model';
import { TasksService } from 'src/services/http/tasks.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  constructor(private taskService: TasksService) { }

  barChartData: BarDataModel[] = [];

  ngOnInit(): void {
    this.fetchDashboard();
  }

  dashboardToBarData(dashboard: any): BarDataModel[] {
    return Object.keys(dashboard).map(key => new BarDataModel(key, dashboard[key]));
  }

  fetchDashboard() {
    this.taskService.dashboard().subscribe(data => {
      this.barChartData = this.dashboardToBarData(data);
    });
  }
}
