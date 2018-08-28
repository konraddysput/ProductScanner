import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Camera, CameraOptions } from "@ionic-native/camera";
import { Observable } from 'rxjs/Observable';
import { PhotoDetailViewModel } from '../../model/photoDetailViewModel';
import { ApiService } from '../api-service/api-service';
@Injectable()
export class PhotoService {
  constructor(
    private readonly apiService: ApiService,
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

  public sendPhoto(file: File) {
    const formData: FormData = new FormData();
    formData.append('file', file, "product-scanner.jpeg");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.apiService.token}`
    });

    return this.http.post<any>(this.apiService.photoUploadUrl, formData, { headers: headers });
  }

  public toFile(photoImage: string) {
    return (fetch(photoImage)
      .then(function (res) { return res.arrayBuffer(); })
      .then(function (buf) { return new File([buf], "filename", { type: "image/jpeg" }); })
    );
  }

  public getPhotos(page: number, limit: number): Observable<PhotoDetailViewModel[]> {
    const photoUrl: string = `${this.apiService.apiBaseUrl}api/photo?page=${page}&limit=${limit}`;
    const headers = this.apiService.authenticationHeader;
    return this.http.get<PhotoDetailViewModel[]>(photoUrl, { headers });
  }

  public getPhoto(id: number): Observable<PhotoDetailViewModel> {
    const photoUrl: string = `${this.apiService.apiBaseUrl}api/photo/${id}`;
    const headers = this.apiService.authenticationHeader;
    return this.http.get<PhotoDetailViewModel>(photoUrl, { headers });
  }


  public deletePhoto(id: number): Observable<void> {
    const deletePhotoUrl: string = `${this.apiService.apiBaseUrl}api/photo/${id}`;
    const headers = this.apiService.authenticationHeader;

    return this.http.delete<void>(deletePhotoUrl, { headers });
  }

}
