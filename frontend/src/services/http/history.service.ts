import { Injectable } from '@angular/core';
import { HttpConfig } from 'src/config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskChange } from 'src/models/task-change.model';

@Injectable()
export class HistoryService {
    constructor(
        private httpConfig: HttpConfig,
        private http: HttpClient
    ) { }

    tasksHistoryOfCurrentUserUrl: string = this.httpConfig.restUrl + 'api/user/tasks/history';

    tasksHistoryOfCurrentUser(): Observable<TaskChange[]> {
        return this.http.get<TaskChange[]>(
            this.tasksHistoryOfCurrentUserUrl
        );
    }
}
