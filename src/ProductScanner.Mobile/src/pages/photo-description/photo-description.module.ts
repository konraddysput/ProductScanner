import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PhotoDescriptionPage } from './photo-description';

@NgModule({
  declarations: [
    PhotoDescriptionPage,
  ],
  imports: [
    IonicPageModule.forChild(PhotoDescriptionPage),
  ],
})
export class PhotoDescriptionPageModule {}
