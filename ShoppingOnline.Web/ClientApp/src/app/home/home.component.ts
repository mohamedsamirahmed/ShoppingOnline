import { Component, OnInit } from '@angular/core';
//import { ActivatedRoute } from '@angular/router';
//import { environment } from '../../environments/environment';
//import { Lookup } from '../../models/lookup';
//import { Pagination } from '../../models/Pagination';
//import { Product } from '../../models/product';
//import { LookupService } from '../../Services/lookup-service';
//import { ProductService } from '../../Services/product-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  registerMode = false;

  constructor() { }

  

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  //public _products: Product[];
  //public _categories: Lookup[];
  //pagination: Pagination;
  //productParams: any = {};

  //serviceBaseUrl = environment.apiEndpoint;
  //categoryServiceEndpoint = this.serviceBaseUrl + 'ProductDashboard/GetCategories';
  //productServiceEndpoint = this.serviceBaseUrl + 'ProductDashboard/GetProducts';

  //constructor(private productService: ProductService, private route: ActivatedRoute, private lookupService: LookupService) { }

  ngOnInit(): void {
    //this.productParams.Category = "";

    //this.loadProducts();

    //Get All Customer Vehicles from service
    //this.loadCategoryLookup(this.categoryServiceEndpoint);
  }


  //resetFilters() {
  //  this.productParams.Category = "";
  //  this.loadProducts();
  //}

  //on change pagining 
  //pageChanged(event: any): void {
  //  this.pagination.currentPage = event.page;
  //  this.loadProducts();
  //}

  //get all current page products
  //loadProducts() {

  //  let pageNumber = null;
  //  let itemsPerPage = null;
  //  if (this.pagination) {
  //    pageNumber = this.pagination.currentPage;
  //    itemsPerPage = this.pagination.itemsPerPage;
  //  }

  //  //Get All Customer Vehicles from service
  //  this.productService.getProductList(pageNumber, itemsPerPage, this.productParams).subscribe((response: any) => {
  //    if (response.result.returnStatus) {
  //      this._products = response.result.entity;
  //      this.pagination = response.pagination
  //    }
  //    else
  //      console.log(response.result.returnMessage);

  //  }, error => {
  //   console.log(error);
  //  });
  //}

  ////get all categories
  //loadCategoryLookup(serviceEndPoint) {
  //  //Get all Customers for lookup filter
  //  this.lookupService.getlookupList(serviceEndPoint).subscribe((response: any) => {
  //    if (response.returnStatus)
  //      this._categories = response.entity;

  //  }, error => {
  //    console.log(error);
  //  });
  //}




}
