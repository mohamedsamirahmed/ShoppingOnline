import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../Services/product-service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;
     
  constructor(private productService: ProductService, private route: ActivatedRoute) {
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

  }

}
