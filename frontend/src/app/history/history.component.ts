import { Component, OnInit } from '@angular/core';
import { HistoryService } from 'src/services/http/history.service';
import { TaskChange } from 'src/models/task-change.model';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
})

export class HistoryComponent implements OnInit {
  constructor(private historyService: HistoryService) {}

  history: TaskChange[];

  ngOnInit(): void {
    this.historyService.getTasksHistoryOfCurrentUser().subscribe((data) => this.history = data);
  }
}
