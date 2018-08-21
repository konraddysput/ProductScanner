import { Component } from '@angular/core';
import { NavController, LoadingController, IonicPage, ToastController } from 'ionic-angular';
import { AuthService } from '../../providers/auth-service/auth-service';

@IonicPage()
@Component({
    selector: "register-page",
    templateUrl: "register.html"    
})
export class RegisterPage {

    public username: string;
    public email: string;
    public password: string;

    constructor(
        public navCtrl: NavController,
        public loadingCtrl: LoadingController,
        public toastCtrl: ToastController,
        public authService: AuthService) {

      }

      public register() {
        const loader = this.loadingCtrl.create({
            content: "Please wait..."
          });
      
          loader.present();

          this.authService.register(this.username, this.email, this.password)
            .subscribe(
                () => {
                    //set current view
                    const activeView = this.navCtrl.getActive().component;
                    this.navCtrl.setRoot(activeView);

                    //display toast
                    const toast = this.toastCtrl.create({
                        message: `Success! Please login to your new account`,
                        duration: 10000,
                        showCloseButton: true,
                        closeButtonText: 'Ok'
                      });
                      loader.dismiss();
                      toast.present();
                },
                (ex) => {
                    const msg = typeof(ex.error) === "string" ? ex.error : "Insert valid data";
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
}