import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class GlobalProvider {
  //base 
  //base url require / in the end of uri
  // public readonly apiBaseUrl = "https://192.168.1.66:44330/";

  public readonly apiBaseUrl = "https://localhost:44330/";
  //photo upload 
  public photoUploadUrl = `${this.apiBaseUrl}api/photo`;

  //authenticaiton
  public readonly registerUrl = `${this.apiBaseUrl}api/auth/register`;
  public readonly loginUrl = `${this.apiBaseUrl}api/auth/token`;

  private readonly localStorageTokenKey = "token";

  get authenticationHeader(): HttpHeaders{
    let headers = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization' : `Bearer ${this.token}`
    });
    return headers;
  }

  get token(): string {
    return localStorage.getItem(this.localStorageTokenKey);
  }
  set token(value: string) {
    localStorage.setItem(this.localStorageTokenKey, value);
  }

  public exceptionMsg(ex: any): string{
    console.warn(ex);
    return typeof (ex.error) !== "string" 
    ? "Something goes wrong. Please try again" 
    : ex.error;
  }


}
