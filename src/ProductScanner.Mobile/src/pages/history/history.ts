import { Component } from '@angular/core';
import { IonicPage, NavController, LoadingController, Loading, ToastController } from 'ionic-angular';
import { PhotoService } from '../../providers/photo-service/photo-service';
import { PhotoDetailViewModel } from '../../model/photoDetailViewModel';
import { GlobalProvider } from '../../providers/global/global';
// import { PhotoDetailComponent } from '../../components/photo-detail/photo-detail';

@IonicPage()
@Component({
  selector: 'page-history',
  templateUrl: 'history.html'
})
export class HistoryPage {

  public limit: number = 10;
  public page: number = 0;

  public photos: PhotoDetailViewModel[];

  private _loader: Loading;

  constructor(
    public global: GlobalProvider,
    public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    public photoService: PhotoService
  ) {
    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
    this._loader.present();
  }

  ionViewDidLoad() {
    this.loadPhotos();
  }
  

  public getPhotoUrl(id: number): string {
    console.log("photoUrl");
    return `${this.global.apiBaseUrl}api/photo/${id}/image`;
  }

  public next(){
    this.page++;
    this.loadPhotos();
  }

  public previous(){
    this.page--;
    this.loadPhotos();
  }

  private loadPhotos(){
    this.photoService.getPhotos(this.page, this.limit)
      .subscribe(
        (data) => {
          this.photos = data;
          this._loader.dismiss();
        },
        (ex) => {
          const msg = this.global.exceptionMsg(ex);
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

  public remove(id: number){
    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
    this._loader.present();
    this.photoService.deletePhoto(id)
      .subscribe(
        () => {
          this.photos = this.photos.filter(n =>n.id != id);
          this._loader.dismiss();
        },
        (ex) => {
          const msg = this.global.exceptionMsg(ex);
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

  public refresh(){
    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
    this._loader.present();
    this.loadPhotos();
    
  }

}
