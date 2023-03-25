import { Component } from '@angular/core';
import { AuthenticationService } from "../authentication.service";
import { LoginForm } from "./login-form";
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: LoginForm = { username: "", password: "" };
  constructor(private readonly authService: AuthenticationService) {
  }

  login(form: NgForm) {
    this.loginForm = form.form.value;
    return this.authService.login(this.loginForm).subscribe();
  }
}
