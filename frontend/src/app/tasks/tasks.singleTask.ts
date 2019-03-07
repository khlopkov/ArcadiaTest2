import { Component, Input } from '@angular/core';
import { Task } from 'src/models/task.model';

@Component({
  selector: 'app-single-task',
  templateUrl: './tasks.singleTask.html',
  styleUrls: ['./tasks.singleTask.scss']
})
export class SingleTaskComponent {
    @Input() task: Task;
    isOverdued(): boolean {
      return this.task &&
       new Task(this.task.id, this.task.title, this.task.description, this.task.status, this.task.dueDate, this.task.type).isOverdued();
    }
}
