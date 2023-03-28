import { Component } from '@angular/core';
import {RegisterForm} from "./register-form";
import {AuthenticationService} from "../authentication.service";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: RegisterForm = { username: "", password: "", confirmPassword: "" };

  constructor(private readonly authService: AuthenticationService) { }

  register(form: NgForm) {
    this.registerForm = form.form.value;
    this.authService.register(this.registerForm).subscribe();
  }
}
