import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpConfig } from '../../config';
import { Task } from 'src/models/task.model';
import { Observable } from 'rxjs';
import { Dashboard } from 'src/models/dashboard.model';
import { TaskChange } from 'src/models/task-change.model';

const JSON_MIME = {'Content-type': 'application/json; charset=utf-8'};

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
        return this.http.get<Task[]>(this.baseUrl + 'api/tasks', { headers: new HttpHeaders(JSON_MIME) });
    }

    post(task: Task) {
        return this.http.post(this.baseUrl + 'api/tasks', task, { headers: new HttpHeaders(JSON_MIME) });
    }

    patch(task: Task) {
        return this.http.patch(this.baseUrl + `api/tasks/${task.id}`, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    delete(task: Task) {
        return this.http.delete(this.baseUrl + `api/tasks/${task.id}`);
    }

    dashboard(): Observable<Dashboard> {
        return this.http.get<Dashboard>(this.baseUrl + `api/tasks/dashboard`, { headers: new HttpHeaders(JSON_MIME)});
    }

    history(): Observable<TaskChange[]> {
        return this.http.get<TaskChange[]>(this.baseUrl + 'api/tasks/history', { headers: new HttpHeaders(JSON_MIME)});
    }

    private jsonMimeHeaders() {
        const headers = new HttpHeaders();
        headers.set('Content-Type', 'application/json; charset=utf-8');
        return headers;
    }
}
