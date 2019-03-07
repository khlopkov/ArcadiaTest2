import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Task } from 'src/models/task.model';

@Component({
  selector: 'app-single-task',
  templateUrl: './tasks.singleTask.html',
  styleUrls: ['./tasks.singleTask.scss']
})
export class SingleTaskComponent {
    @Input() task: Task;
    @Output() afterPatch = new EventEmitter<void>();

    showEditForm = false;

    isOverdued(): boolean {
      return this.task && this.task.dueDate !== null &&
        new Task(this.task.id, this.task.title, this.task.description, this.task.status, this.task.dueDate, this.task.type).isOverdued();
    }
    isEditable(): boolean {
      return this.task.status === 'Active' && !this.isOverdued();
    }
    onEditButtonClick(): void {
      this.showEditForm = true;
    }
    onTaskPatched(): void {
      this.afterPatch.emit();
      this.showEditForm = false;
    }
}
