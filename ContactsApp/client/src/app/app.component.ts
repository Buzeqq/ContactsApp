import {Component} from '@angular/core';
import {AuthenticationService} from "./authentication.service";
import {Observable} from "rxjs";
import {UserInfo} from "./user-info";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
  user$: Observable<UserInfo>;
  constructor(public readonly authService: AuthenticationService) {
    this.user$ = this.authService.user;
  }

  logout() {
    this.authService.logout().subscribe();
  }
}
