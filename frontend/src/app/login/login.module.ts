import { LoginComponent } from './login.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
  ],
  exports: [
    LoginComponent,
  ],
  providers: [],
  bootstrap: [LoginComponent]
})
export class LoginModule { }
