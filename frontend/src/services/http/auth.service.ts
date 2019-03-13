import { Injectable } from '@angular/core';
import { UserService } from './user.service';

@Injectable()
export class AuthService {
  constructor(private userService: UserService) {}
  login(email: string, password: string) {
    localStorage.setItem('token', btoa(`${email}:${password}`));
    return this.userService.getCurrent();
  }

  logout() {
    localStorage.removeItem('token');
  }

  token(): string {
    return localStorage.getItem('token');
  }
}
