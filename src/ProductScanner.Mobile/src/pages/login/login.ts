import { Component } from '@angular/core';
import { IonicPage, NavController, LoadingController, ToastController } from 'ionic-angular';
import { AuthService } from '../../providers/auth-service/auth-service';
import { TabsPage } from '../tabs/tabs';
import { GlobalProvider } from '../../providers/global/global';

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
  host: {
    '(document:keyup)': 'onKeyUp($event)'
  }
})
export class LoginPage {

  public password:string;
  public username: string;

  constructor(
    public navCtrl: NavController,
    public global: GlobalProvider,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    public authService: AuthService) {
  }

  public async login() {
    const loader = this.loadingCtrl.create({
      content: "Please wait..."
    });

    loader.present();
    this.authService.authenticate(this.username ,this.password)
      .subscribe(
        () => {
          this.navCtrl.push(TabsPage);
          loader.dismiss();
        },
        (ex) => {
          const msg = this.global.exceptionMsg(ex);
          const toast = this.toastCtrl.create({
            message: msg,
            duration: 10000,
            showCloseButton: true,
            closeButtonText: 'Ok'
          });
          loader.dismiss();
          toast.present();
        }
      )
  }

  onKeyUp(ev:KeyboardEvent) {
    if (ev.keyCode === 13) {
      this.login();
    }
  }

}
