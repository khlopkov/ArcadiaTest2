import { Component, Input, OnInit } from '@angular/core';
import { Task } from 'src/models/task.model';
import { TasksService } from 'src/services/http/tasks.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
})
export class TasksComponent implements OnInit {

  constructor(private taskService: TasksService) { }

  tasks: Task[] = undefined;

  ngOnInit(): void {
    this.taskService.get().subscribe(data => this.tasks = data, error => console.log(error));
  }
}
