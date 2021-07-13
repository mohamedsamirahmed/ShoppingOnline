import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { CartItem } from '../models/cart-item';
import { Order } from '../models/order';
import { OrderItem } from '../models/order-item';
import { PaginatedResult } from '../models/Pagination';
import { Product } from '../models/product';

//const httpOptions = {
//  headers: new HttpHeaders({ 
//    Authorization: 'Bearer ' + (localStorage.getItem('user')? JSON.parse(localStorage.getItem('user')).token:"")
//  })
//}

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  serviceBaseUrl = environment.apiEndpoint;
  constructor(private http: HttpClient, private toastrService: ToastrService) { }

  getProductList(page?, itemsPerPage?, productParams?): Observable<PaginatedResult<Product[]>> {

    const paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (productParams.Category) {
      params = params.append('Category', productParams.Category);
    }

    return this.http.get<Product[]>(this.serviceBaseUrl + 'ProductDashboard/GetProducts', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }
          return paginatedResult;
        })
      )
  }

  getProduct(procuctId) {

    return this.http.get<Product>(this.serviceBaseUrl + 'ProductDashboard/GetProducts/' + procuctId, { observe: 'response' })
      .pipe(
        map(response => {
          if (response.status == 200) {
            return response.body["entity"];

          } else {
            this.toastrService.error(response["returnMessage"]);
          }
        }));
  }

  AddProductToCart(product:any,username:string) {
    return this.http.post(this.serviceBaseUrl + "ProductDashboard/AddToCart", product);
  }

  getCartItemList(username: string): Observable<CartItem[]> {

    return this.http.get<CartItem[]>(this.serviceBaseUrl + 'ProductDashboard/GetCartItems', { observe: 'response'})
      .pipe(
        map(response => {
           return response["body"];
        })
      )
  }

  removeCartItem(cartItem: CartItem){
    return this.http.put<CartItem>(this.serviceBaseUrl + 'ProductDashboard/RemoveCartItem', cartItem, {  observe: 'response' })
      .pipe(
        map(response => {
          return response;
        })
      )
  }


  OrderItems(username: string) {
    return this.http.post(this.serviceBaseUrl + "ProductDashboard/CartCheckout/", { observe: 'response' });
  }

  deliverOrder(username: string, shipmentAddress: string) {
    return this.http.post(this.serviceBaseUrl + "ProductDashboard/CartCheckout/" + shipmentAddress ,{ observe: 'response' });
  }

  getOrderList(username: string) {

    return this.http.get<OrderItem[]>(this.serviceBaseUrl + 'ProductDashboard/GetOrderItems')
      .pipe(
        map(response => {
          return response;
        })
      )
  }
}
