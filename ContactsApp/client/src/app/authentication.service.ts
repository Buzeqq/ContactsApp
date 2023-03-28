import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable, tap } from "rxjs";
import { LoginForm } from "./login/login-form";
import { RegisterForm } from "./register/register-form";
import { UserInfo } from "./user-info";
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private readonly user$ = new BehaviorSubject<UserInfo>({ username: "", isLogged: false });

  constructor(private readonly http: HttpClient) {
    this.loadUser().subscribe(user => this.user$.next(user));
  }

  loadUser(): Observable<UserInfo> {
    return this.http.get<{
      'AspNet.Identity.SecurityStamp': string,
      'amr': string,
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string,
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string
    }>("/api/user").pipe(
      map(user => {
        let username = user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        if (username === undefined) return { username: "", isLogged: false }
        return { username, isLogged: true }
      }),
    );
  }
  login(loginForm: LoginForm): Observable<never> {
    return this.http.post<never>("/api/login", loginForm, { withCredentials: true }).pipe(
      tap(_ => {
        this.loadUser().subscribe(user => this.user$.next(user));
      })
    );
  }
  register(registerForm: RegisterForm): Observable<never> {
    return this.http.post<never>("/api/register", registerForm, { withCredentials: true }).pipe(
      tap(_ => this.loadUser())
    );
  }

  logout(): Observable<never> {
    this.user$.next({
      username: "",
      isLogged: false
    })
    return this.http.get<never>("/api/logout");
  }

  get user() {
    return this.user$.asObservable();
  }
}
