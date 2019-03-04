import { Component, Input } from '@angular/core';
import { Task } from 'src/models/task.model';

@Component({
  selector: 'app-single-task',
  templateUrl: './tasks.singleTask.html',
  styleUrls: ['./tasks.singleTask.scss']
})
export class SingleTaskComponent {
    @Input() task: Task;
}
