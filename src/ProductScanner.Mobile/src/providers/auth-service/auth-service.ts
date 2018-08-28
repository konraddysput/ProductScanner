import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';
import { ApiService } from '../api-service/api-service';


@Injectable()
export class AuthService {

  constructor(
    public http: HttpClient,
    private readonly apiService: ApiService) {  }

  public authenticate(login: string, password: string): Observable<any> {
    const data = JSON.stringify({
      login,
      password
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(this.apiService.loginUrl, data, { headers: headers })
      .map(response => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
        }
        return response;
      })
  }

  public register(login: string, email: string, password: string) : Observable<any> {
    const data = JSON.stringify({
      login,
      email,
      password
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(this.apiService.registerUrl, data, {headers});
  }

  public logout(){
    localStorage.removeItem('token');
  }
}
