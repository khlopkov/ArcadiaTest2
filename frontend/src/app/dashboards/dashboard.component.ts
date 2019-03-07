import {Component, OnInit} from '@angular/core';

import { Dashboard } from 'src/models/dashboard.model';
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

  dashboardToBarData(dashboard: Dashboard): BarDataModel[] {
    console.log(dashboard);
    const data = Object.keys(dashboard).map(key => new BarDataModel(key, dashboard[key]));
    console.log(data);
    return data;
  }

  fetchDashboard() {
    this.taskService.dashboard().subscribe(data => {
      this.barChartData = this.dashboardToBarData(data);
    });
  }
}
