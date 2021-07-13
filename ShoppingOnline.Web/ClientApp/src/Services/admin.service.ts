import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Order } from '../models/order';
import { PaginatedResult } from '../models/Pagination';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  serviceBaseUrl = environment.apiEndpoint;

  constructor(private http: HttpClient, private toastrService: ToastrService) { }


  getOrders(page?, itemsPerPage?): Observable<PaginatedResult<Order[]>> {

    const paginatedResult: PaginatedResult<Order[]> = new PaginatedResult<Order[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Order[]>(this.serviceBaseUrl + 'admin/ReviewOrders/', { observe: 'response', params })
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

  getOrder(orderId) {

    return this.http.get<Order>(this.serviceBaseUrl + 'admin/review-orders/' + orderId, { observe: 'response' })
      .pipe(
        map(response => {
          if (response.status == 200) {
            return response.body["entity"];

          } else {
            this.toastrService.error(response["returnMessage"]);
          }
        }));
  }

  updateOrder(orderId:number,statusId:number) {

    return this.http.post(this.serviceBaseUrl + 'admin/EditOrders/' + orderId + "/" + statusId, { observe: 'response' })
      .pipe(
        map(response => {
          return response;
        }));
  }
}
