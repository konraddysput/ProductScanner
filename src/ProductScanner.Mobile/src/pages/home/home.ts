import { Component } from '@angular/core';
import { NavController, LoadingController, ToastController, Loading } from 'ionic-angular';
import { PhotoService } from '../../providers/photo-service/photo-service';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  public photo: string;
  private readonly _loader: Loading;

  constructor(
    public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    private readonly photoService: PhotoService) {
    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
  }

  public takePhoto() {
    this.photoService.takePhoto()
      .then(
        imageData => {
          const base64Image = "data:image/jpeg;base64," + imageData;
          this.photo = base64Image;
        },
        (ex) => {
          const msg = typeof (ex.error) !== "string" ? "Something goes wrong. Please try again" : ex.error;
          const toast = this.toastCtrl.create({
            message: msg,
            duration: 10000,
            showCloseButton: true,
            closeButtonText: 'Ok'
          });
          toast.present();
        }
      );
  }

  public async sendPhoto() {
    this._loader.present();
    const file = await this.photoService.toFile(this.photo);

    this.photoService.sendPhoto(file)
      .subscribe(
        () => {
          debugger;
          this._loader.dismiss();
          this.photo = "";
        },
        (ex) => {
          const msg = typeof (ex.error) !== "string" ? "Something goes wrong. Please try again" : ex.error;
          const toast = this.toastCtrl.create({
            message: msg,
            duration: 10000,
            showCloseButton: true,
            closeButtonText: 'Ok'
          });
          this._loader.dismiss();
          toast.present();
        }
      );
  }

  public deletePhoto() {
    this.photo = "";
  }

}
