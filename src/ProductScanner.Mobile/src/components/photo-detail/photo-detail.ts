import { Component, Input, Output, EventEmitter } from '@angular/core';
import { PhotoDetailViewModel } from '../../model/photoDetailViewModel';
import { AlertController, ModalController } from 'ionic-angular';
import { ApiService } from '../../providers/api-service/api-service';
import { PhotoDescriptionPage } from '../../pages/photo-description/photo-description';

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
    public modalCtrl: ModalController,
    public alertCtrl: AlertController,
    public apiService: ApiService
  ) { }

  public photoUrl(id:number): string {
    return `${this.apiService.apiBaseUrl}api/photo/${id}/image`;
  }

  public analysedPhotoUrl(id: number): string {
    return `${this.apiService.apiBaseUrl}api/photo/${id}/analyse`;
  }

  public details(): void{
    const modal = this.modalCtrl.create(PhotoDescriptionPage, {id: this.photo.id, ready: true});
    modal.present();
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
  }}
