import { Component, OnInit } from '@angular/core';
import { TasksService } from 'src/services/http/tasks.service';
import { TaskChange } from 'src/models/task-change.model';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
})

export class HistoryComponent implements OnInit {
  constructor(private taskService: TasksService) {}

  history: TaskChange[];

  ngOnInit(): void {
    this.taskService.history().subscribe((data) => this.history = data);
  }
}
