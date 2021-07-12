import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  serviceBaseUrl = environment.apiEndpoint;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.serviceBaseUrl + "users/login", model).pipe(
      map((response: User) => {
        if (response["returnStatus"] == true) {
          const user = response["entity"];
          if (user) {
            this.setCurrentUser(user);
          }
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.serviceBaseUrl + "users/register", model).pipe(
      map((response: User) => {
        if (response["returnStatus"] == false) return;
        const user = response["entity"];
        if (user) {
          this.setCurrentUser(user);
        }
        return user;


      }));
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }


}
