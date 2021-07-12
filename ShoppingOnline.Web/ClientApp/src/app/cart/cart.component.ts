import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { OrderShipmentaddressModalComponent } from '../../modals/order-shipmentaddress-modal/order-shipmentaddress-modal.component';
import { CartItem } from '../../models/cart-item';
import { AccountService } from '../../Services/account-service';
import { ProductService } from '../../Services/product-service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html'
})

export class CartComponent implements OnInit {

  public _cartItems: CartItem[];
  username: string;
  bsModalRef: BsModalRef;

  constructor(private productService: ProductService, private accountService: AccountService,
    private toastrService: ToastrService, private router: Router, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.username = user["userName"]);

    this.loadCartItems();
  }

  get total() {
    return this._cartItems.reduce((sum, x) =>
    ({
      quantity: 1,
      //priceWhenBought: sum.priceWhenBought //+ x.quantity * x.priceWhenBought
      priceWhenBought: sum.priceWhenBought + x.price
    }),
      { quantity: 1, priceWhenBought: 0 }).priceWhenBought;
  }


  loadCartItems() {

    this.productService.getCartItemList(this.username).subscribe((response: any) => {
      if (response.returnStatus) {
        this._cartItems = response["entity"];
      }
      else {
        this.toastrService.error(response.returnMessage);
      }
    }, error => {
      this.toastrService.error(error.message);
    })
  }

  deleteFromCart(cartItem: CartItem) {
    this.productService.removeCartItem(cartItem).subscribe((response: any) => {
      if (response.status == 200) {
        this._cartItems = this._cartItems.filter(item => item.id !== cartItem.id);
      }
      else {
        this.toastrService.error(response.returnMessage);
      }
    }, error => {
      this.toastrService.error(error.message);
    })
  }

  //CheckoutCart
  checkoutCart() {
    //return this.productService.OrderItems(this.username).subscribe(response => {
    //  if (response.status == 200) {
    //    this.router.navigateByUrl('/delivery');
    //  } else {
    //    this.toastrService.error(response.returnMessage);
    //  }
    //}, error => {
    //  this.toastrService.error(error.message);
    //});
  }

  OpenOrderModal() {

    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        totalPrice: this.total,
        username: this.username
      }
    };

    this.bsModalRef = this.modalService.show(OrderShipmentaddressModalComponent, config);

    this.bsModalRef.content.updateSelectedOrder.subscribe(values => {
      const shipmentAddress = values;
      this.productService.deliverOrder(this.username, shipmentAddress).subscribe(response => {
        this.router.navigateByUrl('/myorder');
        console.log(response);
      })
    })

  }
}

