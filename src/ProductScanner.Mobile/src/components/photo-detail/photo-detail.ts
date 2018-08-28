import { Component, Input, Output, EventEmitter } from '@angular/core';
import { PhotoDetailViewModel } from '../../model/photoDetailViewModel';
import { GlobalProvider } from '../../providers/global/global';
import { AlertController } from 'ionic-angular';

@Component({
  selector: 'photo-detail',
  templateUrl: 'photo-detail.html'
})
export class PhotoDetailComponent {

  @Input()
  photo: PhotoDetailViewModel;

  @Output()
  removeEvent = new EventEmitter<number>();

  constructor(
    public alertCtrl: AlertController,
    public global: GlobalProvider
  ) { }

  public photoUrl(id:number): string {
    return `${this.global.apiBaseUrl}api/photo/${id}/image`;
  }

  public analysedPhotoUrl(id: number): string {
    return `${this.global.apiBaseUrl}api/photo/${id}/analyse`;
  }

  public hoursAgo(){
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
  public delete(): void{
    const confirm = this.alertCtrl.create({
      title: 'Remove photo?',
      message: 'Are you sure you want to delete current photo?',
      buttons: [
        {
          text: 'Disagree',
          handler: () => {
            return;
          }
        },
        {
          text: 'Agree',
          handler: () => {
            this.removeEvent.emit(this.photo.id);
          }
        }
      ]
    });
    confirm.present();    
  }
}
