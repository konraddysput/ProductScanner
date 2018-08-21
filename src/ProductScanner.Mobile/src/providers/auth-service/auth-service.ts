import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable()
export class AuthService {

  private readonly apiUrl: string = "localhost:5000";
  constructor(public http: HttpClient) {
  }

  public async authenticate(userName: string, password: string) {
    const authenticationUrl: string = `${this.apiUrl}\api\auth\token`;
    const data = JSON.stringify({
      login: userName,
      password
    });
    const response = await this.http.post(authenticationUrl,data).toPromise();
    console.log(response);
  }
}
