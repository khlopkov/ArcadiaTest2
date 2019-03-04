import { Component } from '@angular/core';
import { Task } from 'src/models/task.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  task = new Task(1, 'Task', 'description', 'Active', '2018-01-01', 'Feature');
  tasks = new Array<Task>(
    new Task(1, 'Task', 'description', 'Active', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
    new Task(2, 'Task2', 'description2', 'Active2', '2018-01-01', 'Feature'),
  );
}
