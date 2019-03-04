import { Component } from '@angular/core';

@Component({
  selector: 'app-single-task',
  templateUrl: './tasks.singleTask.html',
  styleUrls: ['./tasks.singleTask.scss']
})
export class SingleTaskComponent {
    title = 'kek'
    description = 'description'
    dueDate = '2019-03-03'
    status = 'Active'
    type = 'type'
    isOverdued = () : boolean => {
      const currentDate = new Date();
      const dueDate = new Date(this.dueDate)
      dueDate.setDate(dueDate.getDate() + 1)
      return this.status == 'Active' && dueDate < currentDate
    }
}
