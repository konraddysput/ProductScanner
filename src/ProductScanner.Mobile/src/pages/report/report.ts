import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ApiService } from '../../providers/api-service/api-service';
@Component({
  selector: 'page-report',
  templateUrl: 'report.html',
})
export class ReportPage {

  private _hubConnection: HubConnection;
  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams,
    public apiService: ApiService) {
  }

  async ionViewDidLoad() {
    const huburl: string  = this.apiService.hubUrl;
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(huburl)
      .build();

      this._hubConnection.on("ReportReady", (jsonData) =>  {
        debugger;
        console.log(jsonData);
        // if(id == this.id && ready){
        //     this.ready = ready;
        //     this.loadData();
        // }
      });

      await this._hubConnection.start();
      await this._hubConnection.invoke("RefreshReports");
  }

}
