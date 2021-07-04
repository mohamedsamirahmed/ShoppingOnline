import { Component,EventEmitter,OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../Services/account-service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  constructor(private accountService: AccountService,private router:Router) { }

  ngOnInit():void { }

  register() {
    this.accountService.register(this.model).subscribe(response => {
      //this.router.navigate(['', { id: heroId }]);
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error);
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
