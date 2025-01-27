import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpClientService } from '../helpers/httpClientService';
import { OrderRequest } from '../models/request/orderRequest.model';


@Injectable({
  providedIn: 'root',
})
export class ServicesCustomer extends HttpClientService {

  constructor(httpClient: HttpClient) {
    super(httpClient);
  }

  getCustomers(companyName : string): Observable<any> {
    return this.get<any>(`Customers/GetList/${companyName}`);
  }

  getOrderByCustomers(customerId: string): Observable<any> {
    return this.get<any>(`Orders/getOrdersByCustomers/${customerId}`);
  }

  createOrder(request: OrderRequest): Observable<any> {
    return this.post<OrderRequest, any>("Orders", request);
  }
}
