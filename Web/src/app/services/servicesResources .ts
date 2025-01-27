import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpClientService } from '../helpers/httpClientService';
import { SelectItem } from '../models/common/selectItem.model';


@Injectable({
  providedIn: 'root',
})
export class ServicesResources  extends HttpClientService {

  constructor(httpClient: HttpClient) {
    super(httpClient);
  }

  getProducts(): Observable<any> {
    return this.get<SelectItem>("Products/GetList");
  }

  getShippers(): Observable<any> {
    return this.get<SelectItem>("Shippers/GetList");
  }

  getEmployees(): Observable<any> {
    return this.get<SelectItem>("Employees/GetList");
  }
}
