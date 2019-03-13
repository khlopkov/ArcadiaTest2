import { REST_URL } from '../../config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/models/user.model';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
    private baseUrl: string = REST_URL;
    constructor(
        private http: HttpClient
    ) { }

    getCurrent(): Observable<User> {
        return this.http.get<User>(this.baseUrl + 'api/user');
    }
}
