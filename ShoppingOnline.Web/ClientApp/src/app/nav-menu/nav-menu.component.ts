import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(public accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService
    ) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {

      console.log(response);
      this.router.navigateByUrl('/products');
    }, error => {
      console.log(error);
      //this.toastrService.error(error.error);
    });

  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }


}
