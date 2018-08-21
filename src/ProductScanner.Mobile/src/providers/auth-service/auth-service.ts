import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';


@Injectable()
export class AuthService {

  private readonly apiUrl: string = "https://localhost:44330";
  constructor(public http: HttpClient) {
  }

  public authenticate(userName: string, password: string): Observable<any> {
    const authenticationUrl: string = `${this.apiUrl}/api/auth/token`;
    const data = JSON.stringify({
      login: userName,
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

  public logout(){
    localStorage.removeItem('token');
  }
}
