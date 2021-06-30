import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Lookup } from '../models/lookup';

@Injectable()

export class LookupService {

  serviceBaseUrl = environment.apiEndpoint;

  constructor(private http: HttpClient) { }

  //get all lookup values
  getlookupList(serviceEndPoint): Observable<Lookup[]> {
    return this.http.get<Lookup[]>(serviceEndPoint);
  }

}
