import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { TasksService } from 'src/services/http/tasks.service';
import { Task } from 'src/models/task.model';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { dateTodayOrAfterValidator } from 'src/validator/date.validator';

@Component({
  selector: 'app-new-task-form',
  templateUrl: './tasks.createForm.html',
  styleUrls: ['./tasks.form.scss']
})
export class CreateTaskFormComponent implements OnInit {
  constructor(private taskService: TasksService) { }
  @Output() afterPost = new EventEmitter<void>();

  createForm: FormGroup;

  ngOnInit() {
    this.createForm = new FormGroup({
      title: new FormControl('', Validators.required),
      description: new FormControl(''),
      dueDate: new FormControl('', dateTodayOrAfterValidator),
      type: new FormControl('')
    });
  }

  get title() {
    return this.createForm.get('title');
  }
  get description() {
    return this.createForm.get('description');
  }
  get dueDate() {
    return this.createForm.get('dueDate');
  }
  get type() {
    return this.createForm.get('type');
  }

  done(): void {
    this.afterPost.emit();
  }

  createTask() {
    if (this.createForm.invalid) {
      return;
    }
    const task = new Task(undefined, this.title.value, this.description.value, undefined, this.dueDate.value, this.type.value);
    this.taskService.post(task).subscribe(() => { this.done(); } );
  }
}
