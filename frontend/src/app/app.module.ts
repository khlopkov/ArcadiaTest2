import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskModule } from './tasks/tasks.module';
import { HTTP_INTERCEPTORS, HttpClientModule, HttpClient } from '@angular/common/http';
import { BasicAuthInterceptor } from 'src/services/http/basic-auth.interceptor';
import { ErrorInterceptor } from 'src/services/http/error.interceptor';
import { AuthService } from 'src/services/http/auth.service';
import { UserService } from 'src/services/http/user.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    TaskModule
  ],
  providers: [
    HttpClient,
    UserService,
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: BasicAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
