import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Task } from 'src/models/task.model';
import { TasksService } from 'src/services/http/tasks.service';

@Component({
  selector: 'app-single-task',
  templateUrl: './tasks.singleTask.html',
  styleUrls: ['./tasks.singleTask.scss']
})
export class SingleTaskComponent {
  constructor(private taskService: TasksService) {}
  @Input() task: Task;
  @Output() afterPatch = new EventEmitter<void>();
  @Output() afterDelete = new EventEmitter<void>();

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
  onDeleteButtonClick(): void {
    this.taskService.delete(this.task)
      .subscribe(() => this.afterDelete.emit());
  }
  onTaskPatched(): void {
    this.afterPatch.emit();
    this.showEditForm = false;
  }
}
