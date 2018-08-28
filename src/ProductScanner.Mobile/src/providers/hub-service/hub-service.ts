import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

/*
  Generated class for the HubServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class HubService {

  constructor(public http: HttpClient) {
    debugger;
    console.log('Hello HubServiceProvider Provider');
  }

}
