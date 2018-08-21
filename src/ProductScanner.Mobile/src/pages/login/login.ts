import { Component } from '@angular/core';
import { IonicPage, NavController, LoadingController } from 'ionic-angular';
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
    public loadingCtrl: LoadingController,
    public authService: AuthService) {
  }

  public async login() {
    const loader = this.loadingCtrl.create({
      content: "Please wait..."
    });

    loader.present();

    this.authService.authenticate("konrad", "test!@#ZXC")
      .subscribe(
        () => {
          this.navCtrl.push(TabsPage);
          loader.dismiss();
        },
        err => {
          console.log(err);
          loader.dismiss();
        }
      )

  }

}
