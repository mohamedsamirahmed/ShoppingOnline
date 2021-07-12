import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../Services/product-service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../models/product';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../Services/account-service';
import { map, take } from 'rxjs/operators';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;
  userName: string;
  constructor(private productService: ProductService, private route: ActivatedRoute,
    private toastrService: ToastrService, private accountService: AccountService) {
  }
  ngOnInit() {
    this.getProduct();
  }

  getProduct() {
    return this.productService.getProduct(this.route.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
    });
  }

  AddToCart() {

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      console.log(user);
      this.userName = user["userName"];
    });
   
    //var username = currentUser.userName;
    return this.productService.AddProductToCart(this.product, this.userName).subscribe(response => {
      this.toastrService.success("item added to cart!");
    }, error => {
      this.toastrService.error(error.message);
    });
  }

}
