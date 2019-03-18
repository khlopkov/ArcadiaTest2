import { HttpConfig } from '../../config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/models/user.model';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
    constructor(
        private httpConfig: HttpConfig,
        private http: HttpClient
    ) { }

    private get baseUrl(): string {
        return this.httpConfig.restUrl;
    }

    getCurrent(): Observable<User> {
        return this.http.get<User>(this.baseUrl + 'api/user');
    }
}
