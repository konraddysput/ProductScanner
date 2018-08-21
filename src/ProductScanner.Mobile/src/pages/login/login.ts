import { Component } from '@angular/core';
import { IonicPage, NavController } from 'ionic-angular';
import { AuthService } from '../../providers/auth-service/auth-service';
import { TabsPage } from '../tabs/tabs';

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html'
})
export class LoginPage {

  constructor(
    public navCtrl: NavController, 
    public authService: AuthService
  ) {
  }

  public async login(){
     const result = await this.authService.authenticate("konrad", "test!@#ZXC");
     
     if(!result){
       this.navCtrl.push(TabsPage);
     }
  }

}
