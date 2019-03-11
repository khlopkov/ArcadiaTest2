export class Task {
    id: number;
    title: string;
    description: string;
    status: string;
    dueDate: string;
    type: string;

    constructor(
        id: number,
        title: string,
        description: string,
        status: string,
        dueDate: string,
        type: string
    ) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.status = status;
        this.dueDate = dueDate;
        this.type = type;
    }

    isOverdued(): boolean {
      const currentDate = new Date();
      const dueDate = new Date(this.dueDate);
      dueDate.setDate(dueDate.getDate() + 1);
      return this.status === 'Active' && dueDate < currentDate;
    }
}
