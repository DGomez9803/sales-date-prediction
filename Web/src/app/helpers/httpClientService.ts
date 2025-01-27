import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { httpOptions } from './httpOptions';
import { Injectable } from '@angular/core';
/**
 * This is the http client base-service. It use the HttpClient module to make HTTP requests.
 * The HttpClient's generic methods ensure that strongly typed objects are returned to components that use the service.
 * Now each module should implement a service extended from this class
 */

@Injectable({
  providedIn: 'root',
})

export class HttpClientService {

  constructor(
    protected httpClient: HttpClient,
  ) {}

  /**
   * Method which sends a POST request to the server
   */
  protected post<T, U>( endpoint: string, command?: T): Observable<U> {
    var fullEndpoint = this.getFullEndpointWithVersion(
      endpoint
    );

    return this.httpClient.post<U>(
      fullEndpoint,
      command
    );
  }

  /**
   * Method which sends a GET request to the server
   */
  protected get<U>(endpoint: string): Observable<U> {
    const options = {
      headers: httpOptions.headers,
      params: new HttpParams()
    };

    var fullEndpoint = this.getFullEndpointWithVersion(
      endpoint
    );

    return this.httpClient.get<U>(
      fullEndpoint,
      options
    );
  }

  private getFullEndpointWithVersion( endpoint: string): string {
    let url = this.getUrl();

    if(endpoint.indexOf('/') == 0) {
      endpoint = endpoint.substring(endpoint.indexOf('/') + 1, endpoint.length);
    }

    return `${url}/${endpoint}`;
  }

  private getUrl(): string {
    let url: string =  environment.apiUrl;

    if (url.lastIndexOf("/") == url.length - 1) {
      url = url.substring(0, url.length - 1);
    }

    return url;
  }
}
