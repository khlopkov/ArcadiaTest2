import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { REST_URL } from '../../config';
import { Task } from 'src/models/task.model';
import { Observable } from 'rxjs';
import { Dashboard } from 'src/models/dashboard.model';
import { TaskChange } from 'src/models/task-change.model';

const JSON_MIME = {'Content-type': 'application/json; charset=utf-8'};

const PREFIX = 'api/user/tasks';

@Injectable()
export class TasksService {

    private baseUrlWithPrefix: string = REST_URL + PREFIX;

    constructor(
        private http: HttpClient
    ) { }

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

    dashboard(): Observable<Dashboard> {
        return this.http.get<Dashboard>(this.baseUrlWithPrefix + `/dashboard/byStatus`, { headers: new HttpHeaders(JSON_MIME)});
    }

    history(): Observable<TaskChange[]> {
        return this.http.get<TaskChange[]>(this.baseUrlWithPrefix + '/history', { headers: new HttpHeaders(JSON_MIME)});
    }
}
