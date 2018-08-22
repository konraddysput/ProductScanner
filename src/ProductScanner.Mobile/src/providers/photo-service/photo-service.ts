import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Camera, CameraOptions } from "@ionic-native/camera";
import { GlobalProvider } from '../global/global';
@Injectable()
export class PhotoService {
  constructor(
    private readonly global: GlobalProvider,
    private readonly camera: Camera,
    public http: HttpClient) { }

  public async takePhoto(): Promise<any> {
    const options: CameraOptions = {
      quality: 50,
      destinationType: this.camera.DestinationType.DATA_URL,
      encodingType: this.camera.EncodingType.JPEG,
      mediaType: this.camera.MediaType.PICTURE,
      targetWidth: 600,
      targetHeight: 600,
      saveToPhotoAlbum: false
    };

    return this.camera.getPicture(options);
  }

  public sendPhoto(file:File) {
      const formData: FormData = new FormData();
      formData.append('file', file, "product-scanner.jpeg");
      const headers = new HttpHeaders({ 
        'Authorization' : `Bearer ${this.global.token}`
      });

     return this.http.post<boolean>(this.global.photoUploadUrl, formData, { headers: headers});
  }

  public toFile(photoImage: string) {
    return (fetch(photoImage)
      .then(function (res) { return res.arrayBuffer(); })
      .then(function (buf) { return new File([buf], "filename", { type: "image/jpeg" }); })
    );
  }

}
