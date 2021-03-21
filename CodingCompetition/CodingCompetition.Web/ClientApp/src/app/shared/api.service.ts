import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, EMPTY, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ApiService {

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient,
    private router: Router) {
  }

  get<T>(url: string, params?: HttpParams, responseType = 'json'): Observable<T> {
    return this.intercept<any>(this.http.get<any>(this.baseUrl + 'api/' + url, { params: params, responseType: responseType as 'json' }));
  }

  post<T>(url: string, body: Object, responseType = 'json'): Observable<T> {
    return this.intercept<any>(this.http.post<any>(this.baseUrl + 'api/' + url, body, { responseType: responseType as 'json' }));
  }

  postFile<T>(url: string, file: FormData): Observable<T> {
    return this.intercept<any>(this.http.post<any>(this.baseUrl + 'api/' + url, file));
  }

  put<T>(url: string, body: Object): Observable<T> {
    return this.intercept<any>(this.http.put<any>(this.baseUrl + 'api/' + url, body));
  }

  delete<T>(url: string): Observable<T> {
    return this.intercept<any>(this.http.delete<any>(this.baseUrl + 'api/' + url));
  }

  intercept<T>(observable: Observable<T>): Observable<T> {
    return observable.pipe(
      catchError(error => {
        if (error.status === 401) {
          this.router.navigateByUrl('/');

          ApiService.printError(error);
          return EMPTY;
        }
        else if (error.status === 403) {
          this.router.navigateByUrl('/403');
          return EMPTY;
        }
        else if (error.status === 404) {
          ApiService.printError(error);
          return throwError(error);
        }
        if (error.status === 409) {
          ApiService.printError(error);
          return EMPTY;
        }
        else {
          ApiService.printError(error);
          return throwError(error);
        }
      })
    );
  }

  private static printError(error: any) {
    const errorMessage = (error.message)
      ? error.message
      : error.status ? `${error.status} - ${error.statusText}` : 'Server error';

    console.error(errorMessage);
  }

  static encodeEmail(email: any) {
    return email.replace(/\+/gi, '%2B');
  }
}
