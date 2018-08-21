import { Component } from '@angular/core';
import { NavController, LoadingController } from 'ionic-angular';
import { RegisterPage } from '../register/register';
import { LoginPage } from '../login/login';
import { AuthService } from '../../providers/auth-service/auth-service';

@Component({
  selector: 'page-welcome',
  templateUrl: 'welcome.html',
})
export class WelcomePage {
  registerTab = RegisterPage;
  loginTab = LoginPage;

  constructor(
      public navCtrl: NavController,
      public loadingCtrl: LoadingController,
      public authService: AuthService) {
    }    

}
