import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpConfig } from '../../config';
import { Task } from 'src/models/task.model';
import { Observable } from 'rxjs';
import { TaskChange } from 'src/models/task-change.model';

const JSON_MIME = {'Content-type': 'application/json; charset=utf-8'};

const PREFIX = 'api/user/tasks';

@Injectable()
export class TasksService {
    constructor(
        private httpConfig: HttpConfig,
        private http: HttpClient
    ) { }

    private get baseUrlWithPrefix(): string {
        return this.httpConfig.restUrl + PREFIX;
    }

    get(): Observable<Task[]> {
        return this.http.get<Task[]>(this.baseUrlWithPrefix, { headers: new HttpHeaders(JSON_MIME) });
    }

    post(task: Task) {
        return this.http.post(this.baseUrlWithPrefix, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    patch(task: Task) {
        return this.http.patch(this.baseUrlWithPrefix + `/${task.id}`, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    delete(task: Task) {
        return this.http.delete(this.baseUrlWithPrefix + `/${task.id}`);
    }

    history(): Observable<TaskChange[]> {
        return this.http.get<TaskChange[]>(this.baseUrlWithPrefix + '/history', { headers: new HttpHeaders(JSON_MIME)});
    }
}
