import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/http/auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  loginForm: FormGroup;

  ngOnInit() {
    this.authService.logout();
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('')
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    this.authService.login(this.email.value, this.password.value)
      .subscribe(
        () => { this.router.navigate(['']); },
        error => { console.log(error); }
      );
  }
}
