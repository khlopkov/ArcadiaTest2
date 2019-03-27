import { Injectable } from '@angular/core';
import { HttpConfig } from 'src/config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const PREFIX =  'api/user/tasks';

export interface TasksCountByStatusDictionary {
    [key: string]: number;
}

@Injectable()
export class StatisticsService {
    constructor(
        private httpConfig: HttpConfig,
        private http: HttpClient
    ) { }

    private readonly baseUrl: string = this.httpConfig.restUrl;

    getStatisticsTasksCountByStatus(): Observable<TasksCountByStatusDictionary> {
        return this.http.get<{ [ key: string ]: number }>(
            this.baseUrl + PREFIX + `/dashboard/byStatus`
        );
    }
}
