import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';


@Injectable()
export class AuthService {

  private readonly apiUrl: string = "https://localhost:44330";
  constructor(public http: HttpClient) {
  }

  public authenticate(login: string, password: string): Observable<any> {
    const authenticationUrl: string = `${this.apiUrl}/api/auth/token`;
    const data = JSON.stringify({
      login,
      password
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(authenticationUrl, data, { headers: headers })
      .map(response => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
        }
        return response;
      })
  }

  public register(login: string, email: string, password: string) : Observable<any> {
    const registerUrl: string = `${this.apiUrl}/api/auth/register`;
    const data = JSON.stringify({
      login,
      email,
      password
    });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(registerUrl, data, {headers});
  }

  public logout(){
    localStorage.removeItem('token');
  }
}
