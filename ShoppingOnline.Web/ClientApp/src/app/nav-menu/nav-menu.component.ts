import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/user';
import { AccountService } from '../../Services/account-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  model: any = {}
  registerMode = false;
  currentUser$: Observable<User>;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
      
    }, error => {
      console.log(error);
    });

  }

  logout() {
    this.accountService.logout();
  }


}
