import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { OrderItem } from '../../models/order-item';
import { AccountService } from '../../Services/account-service';
import { ProductService } from '../../Services/product-service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  public _orders: OrderItem[];
  username: string;

  constructor(private productService: ProductService, private toastrService: ToastrService
    , private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.username = user["userName"]);

    this.loadOrders();
  }

  
  loadOrders() {

    this.productService.getOrderList(this.username).subscribe((response: any) => {
      if (response.returnStatus) {
        this._orders = response.entity;
      }
      else {
        this.toastrService.error(response.returnMessage);
      }
    }, error => {
      this.toastrService.error(error.message);
    });
  }

}
