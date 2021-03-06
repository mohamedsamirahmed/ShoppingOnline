import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment';
import { Lookup } from '../../models/lookup';
import { Pagination } from '../../models/Pagination';
import { Product } from '../../models/product';
import { LookupService } from '../../Services/lookup-service';
import { ProductService } from '../../Services/product-service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  public _products: Product[];
  public _categories: Lookup[];
  pagination: Pagination;
  productParams: any = {};

  serviceBaseUrl = environment.apiEndpoint;
  categoryServiceEndpoint = this.serviceBaseUrl + 'ProductDashboard/GetCategories';
  productServiceEndpoint = this.serviceBaseUrl + 'ProductDashboard/GetProducts';

  constructor(private productService: ProductService, private route: ActivatedRoute,
    private lookupService: LookupService, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.productParams.Category = "";

    this.loadProducts();

    this.loadCategoryLookup(this.categoryServiceEndpoint);
  }
  resetFilters() {
    this.productParams.Category = "";
    this.loadProducts();
  }

  //on change pagining
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadProducts();
  }

  //get all current page products
  loadProducts() {

    let pageNumber = null;
    let itemsPerPage = null;
    if (this.pagination) {
      pageNumber = this.pagination.currentPage;
      itemsPerPage = this.pagination.itemsPerPage;
    }

    
    this.productService.getProductList(pageNumber, itemsPerPage, this.productParams).subscribe((response: any) => {
      if (response.result.returnStatus) {
        this._products = response.result.entity;
        this.pagination = response.pagination
      }
      else {
        console.log(response.result.returnMessage);
        this.toastrService.error(response.result.returnMessage);
      }
    }, error => {
      console.log(error);
      this.toastrService.error(error.message);
    });
  }

  //get all categories
  loadCategoryLookup(serviceEndPoint) {
    
    this.lookupService.getlookupList(serviceEndPoint).subscribe((response: any) => {
      if (response.returnStatus)
        this._categories = response.entity;
    }, error => {
      console.log(error);
      this.toastrService.error(error.message);
    });
  }
}

