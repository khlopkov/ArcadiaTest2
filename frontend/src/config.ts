import { Injectable } from '@angular/core';

const REST_URL = 'http://localhost:52169/';

@Injectable()
export class HttpConfig {
  public get restUrl(): string {
    return REST_URL;
  }
}