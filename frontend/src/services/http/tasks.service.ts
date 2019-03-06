import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { REST_URL } from '../../config';
import { Task } from 'src/models/task.model';
import { Observable } from 'rxjs';
import { Dashboard } from 'src/models/dashboard.model';

const JSON_MIME = {'Content-type': 'application/json; charset=utf-8'};

@Injectable()
export class TasksService {

    private baseUrl: string = REST_URL;
    constructor(
        private http: HttpClient
    ) { }

    get(): Observable<Task[]> {
        return this.http.get<Task[]>(this.baseUrl + 'api/tasks', { headers: new HttpHeaders(JSON_MIME) });
    }

    post(task: Task) {
        return this.http.post(this.baseUrl + 'api/tasks', task, { headers: new HttpHeaders(JSON_MIME) });
    }

    patch(task: Task) {
        return this.http.patch(this.baseUrl + `api/tasks/${task.id}`, task, { headers: new HttpHeaders(JSON_MIME) });
    }

    dashboard(): Observable<Dashboard> {
        return this.http.get<Dashboard>(this.baseUrl + `api/tasks/dashboard`, { headers: new HttpHeaders(JSON_MIME)});
    }

    private jsonMimeHeaders() {
        const headers = new HttpHeaders();
        headers.set('Content-Type', 'application/json; charset=utf-8');
        return headers;
    }
}
