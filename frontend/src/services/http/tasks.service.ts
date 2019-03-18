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

    private get baseUrl(): string {
        return this.httpConfig.restUrl;
    }

    get(): Observable<Task[]> {
        return this.http.get<Task[]>(this.baseUrl + PREFIX, { headers: new HttpHeaders(JSON_MIME) });
    }

    post(task: Task) {
        return this.http.post(this.baseUrl + PREFIX, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    patch(task: Task) {
        return this.http.patch(this.baseUrl + PREFIX + `/${task.id}`, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    delete(task: Task) {
        return this.http.delete(this.baseUrl + PREFIX + `/${task.id}`);
    }

    dashboard(): Observable<{ [ key: string ]: number }> {
        return this.http.get<{ [ key: string ]: number }>(
            this.baseUrl + PREFIX + `/dashboard/byStatus`, { headers: new HttpHeaders(JSON_MIME)}
        );
    }

    history(): Observable<TaskChange[]> {
        return this.http.get<TaskChange[]>(this.baseUrl + PREFIX + '/history', { headers: new HttpHeaders(JSON_MIME)});
    }
}
