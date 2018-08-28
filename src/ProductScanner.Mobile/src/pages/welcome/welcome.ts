import { Component } from '@angular/core';
import { RegisterPage } from '../register/register';
import { LoginPage } from '../login/login';

@Component({
  selector: 'page-welcome',
  templateUrl: 'welcome.html',
})
export class WelcomePage {
  registerTab = RegisterPage;
  loginTab = LoginPage;

  constructor() { }
}
