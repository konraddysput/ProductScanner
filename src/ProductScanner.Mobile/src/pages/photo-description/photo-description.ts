import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, Loading, ViewController, ToastController } from 'ionic-angular';

import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { PhotoService } from '../../providers/photo-service/photo-service';
import { PhotoDetailViewModel } from '../../model/photoDetailViewModel';
import { ExceptionFormater } from '../../helpers/exception-formater';
import { ApiService } from '../../providers/api-service/api-service';
@IonicPage()
@Component({
  selector: 'page-photo-description',
  templateUrl: 'photo-description.html',
})
export class PhotoDescriptionPage {

  public id: number;
  public ready: boolean;
  
  public photo: PhotoDetailViewModel;
  
  private _loader: Loading;

  private _hubConnection: HubConnection;

  constructor(
    public navCtrl: NavController,
    public apiService: ApiService,
    public photoService: PhotoService,
    public viewCtrl: ViewController,
    public toastCtrl: ToastController,
    public loadingCtrl: LoadingController,
    public navParams: NavParams) {

      this._loader = this.loadingCtrl.create({
        content: "Please wait..."
      });
      this._loader.present();

    this.id = navParams.get("id");
    this.ready = navParams.get("ready");
  }

  async ionViewDidLoad() {
    //if data is still in processing algorithm
    //page have to setup signalR hub and wait for server response
    //if server response or data wont be available after 30 sec
    //description will be enable
    if (!this.ready) {
      await this.setupHub();
      return;
    }
    this.loadData();
  }

  private async setupHub(){
    const huburl: string  = this.apiService.hubUrl;
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(huburl)
      .build();

      this._hubConnection.on("DataReady", (id, ready) =>  {
        if(id == this.id && ready){
            this.ready = ready;
            this.loadData();
        }
      });

      await this._hubConnection.start();
      await this._hubConnection.invoke("IsDataReady", this.id);
  }

  public loadData(){
    this.photoService.getPhoto(this.id)
      .subscribe(
        (data) => {
          debugger;
          this.photo = data;
          this._loader.dismiss();
        },
        (ex) => {
          const msg = ExceptionFormater.exceptionMsg(ex);
          const toast = this.toastCtrl.create({
            message: msg,
            duration: 10000,
            showCloseButton: true,
            closeButtonText: 'Ok'
          });
          this._loader.dismiss();
          toast.present();
        }

      )
  }

  public photoUrl(id:number): string {
    return `${this.apiService.apiBaseUrl}api/photo/${id}/image`;
  }

  public analysedPhotoUrl(id: number): string {
    return `${this.apiService.apiBaseUrl}api/photo/${id}/analyse`;
  }

  public dismiss(): void {
    this.viewCtrl.dismiss();
  }

  public delete(): void { 
    this._loader = this.loadingCtrl.create({
      content: "Please wait..."
    });
    this._loader.present();
    
    this.photoService.deletePhoto(this.photo.id)
      .subscribe(
        () => {
          this.dismiss();
          const toast = this.toastCtrl.create({
            message: "File deleted successfully",
            duration: 10000,
            showCloseButton: true,
            closeButtonText: 'Ok'
          });
          this._loader.dismiss();
          toast.present();
        },
        (ex) => {
          const msg = ExceptionFormater.exceptionMsg(ex);
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


  public hoursAgo(){
    try{
    //detect minutes
    const date = new Date(this.photo.uploadDate);
    var delta = Math.abs((date as any) - (new Date() as any)) / 1000;

    // calculate (and subtract) whole days
    var days = Math.floor(delta / 86400);
    if(days > 0){
      return `uploaded ${days} days ago`;
    }

    // calculate (and subtract) whole hours
    var hours = Math.floor(delta / 3600) % 24;
    if(hours > 0){
      return `uploaded ${hours} hours ago`;
    }

    // calculate (and subtract) whole minutes
    var minutes = Math.floor(delta / 60) % 60;
    if(minutes > 0){
      return `uploaded ${hours} minutes ago`;
    }

    // what's left is seconds
    var seconds = delta % 60; 
    return `uploaded ${seconds} minutes ago`;
  }
  catch(err){
    console.error(err);
  }
  }

}
