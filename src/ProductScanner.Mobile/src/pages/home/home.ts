import { Component } from '@angular/core';
import { NavController, LoadingController, ToastController, Loading, ModalController } from 'ionic-angular';
import { PhotoService } from '../../providers/photo-service/photo-service';
import { AuthService } from '../../providers/auth-service/auth-service';
import { ApiService } from '../../providers/api-service/api-service';
import { PhotoDescriptionPage } from '../photo-description/photo-description';
import { ExceptionFormater } from '../../helpers/exception-formater';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  public photo: string;
  private readonly _loader: Loading;

  constructor(
    public modalCtrl: ModalController,
    public authService: AuthService,
    public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    public apiService: ApiService,
    public toastCtrl: ToastController,
    private readonly photoService: PhotoService) {

    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
  }

  public takePhoto() {
    const cameraSourceType = 1;
    this.photoService.takePhoto(cameraSourceType)
      .then(
        imageData => {
          const base64Image = "data:image/jpeg;base64," + imageData;
          this.photo = base64Image;
        },
        (ex) => {
          const msg = ExceptionFormater.exceptionMsg(ex);
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

  public selectPhotoFromGallery(){
    const gallerySourceType = 0;
    this.photoService.takePhoto(gallerySourceType)
      .then(
        imageData => {
          const base64Image = "data:image/jpeg;base64," + imageData;
          this.photo = base64Image;
        },
        (ex) => {
          const msg = ExceptionFormater.exceptionMsg(ex);
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
        (data) => {
          this._loader.dismiss();
          this.deletePhoto();
          const modal = this.modalCtrl.create(PhotoDescriptionPage, { id: data.id, ready: false });
          modal.present();
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

  public logout() {
    this.authService.logout();
    window.location.reload();
  }
}
