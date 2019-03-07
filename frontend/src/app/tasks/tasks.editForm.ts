import { Component, Output, EventEmitter, OnInit, Input } from '@angular/core';
import { TasksService } from 'src/services/http/tasks.service';
import { Task } from 'src/models/task.model';
import { FormGroup, Validators, FormControl, FormArray } from '@angular/forms';
import { dateTodayOrAfterValidator } from 'src/validator/date.validator';

@Component({
  selector: 'app-edit-task-form',
  templateUrl: './tasks.editForm.html',
  styleUrls: ['./tasks.form.scss']
})
export class EditTaskFormComponent implements OnInit {
  constructor(private taskService: TasksService) { }
  @Output() afterPatch = new EventEmitter<void>();
  @Input() task: Task;

  editForm: FormGroup;
  private possibleStatus = ['Active', 'Resolved', 'Cancelled'];

  ngOnInit() {
    this.editForm = new FormGroup({
      title: new FormControl(this.task.title, Validators.required),
      description: new FormControl(this.task.description),
      dueDate: new FormControl(this.task.dueDate && this.task.dueDate.substring(0, 10), dateTodayOrAfterValidator),
      type: new FormControl(this.task.type),
      status: new FormControl(this.task.status)
    });
  }

  get title() {
    return this.editForm.get('title');
  }
  get description() {
    return this.editForm.get('description');
  }
  get dueDate() {
    return this.editForm.get('dueDate');
  }
  get type() {
    return this.editForm.get('type');
  }
  get status() {
    return this.editForm.get('status');
  }

  done(): void {
    this.afterPatch.emit();
  }

  updateTask() {
    if (this.editForm.invalid) {
      return;
    }
    const sentDate = this.task.dueDate !== this.dueDate.value &&
      (this.dueDate.value === '' || this.dueDate.value === null || this.dueDate.value === undefined)
        ? '0001-01-01' : this.dueDate.value;
    const task = new Task(this.task.id, this.title.value, this.description.value, this.status.value, sentDate, this.type.value);
    this.taskService.patch(task).subscribe(() => { this.done(); } );
  }
}
