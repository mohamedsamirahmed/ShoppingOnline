import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { AccountService } from '../Services/account-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  registerMode: boolean;
  title = 'Shopping Online';
  users: any;
  constructor(private http: HttpClient, private accountServive: AccountService) { }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem("user"));
    this.accountServive.setCurrentUser(user);
  }

  
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
