import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { PaginatedResult } from '../models/Pagination';
import { Product } from '../models/product';


@Injectable()

export  class ProductService {

  serviceBaseUrl = environment.apiEndpoint;
  constructor(private http: HttpClient) { }



  //Get customer vehicles based on paging parameter
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

}
