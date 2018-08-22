import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';
import { GlobalProvider } from '../global/global';


@Injectable()
export class AuthService {

  constructor(
    public http: HttpClient,
    private readonly globals: GlobalProvider) {  }

  public authenticate(login: string, password: string): Observable<any> {
    const data = JSON.stringify({
      login,
      password
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(this.globals.loginUrl, data, { headers: headers })
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

    return this.http.post<any>(this.globals.registerUrl, data, {headers});
  }

  public logout(){
    localStorage.removeItem('token');
  }
}
