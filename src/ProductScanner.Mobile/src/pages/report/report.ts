import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ApiService } from '../../providers/api-service/api-service';
@Component({
  selector: 'page-report',
  templateUrl: 'report.html',
})
export class ReportPage {
  public productNameData: number[] = [0];
  public productNameLabels: string[] = ["Pepsi", "CocaCola", "CocaColaZero"];

  public doughnutChartType: string = "doughnut";

  public validationData: number[] = [0, 0];
  public validationLabels: string[] = ["Invalid", "Valid"]

  private _hubConnection: HubConnection;
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public apiService: ApiService) {
  }

  async ionViewDidLoad() {
    const huburl: string = this.apiService.hubUrl;
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(huburl)
      .build();

    this._hubConnection.on("ReportReady", (jsonData) => {
      const data = JSON.parse(jsonData);
      const validationArrayCopy = [];
      validationArrayCopy[0] = data.TotalNumberOfInvalidIndividuals;
      validationArrayCopy[1] = data.TotalNumberOfIndividuals - validationArrayCopy[0];
      this.validationData = validationArrayCopy;
      if(data.DetectedProducts != null){
        const labelsMap = [];
        const dataArray = [];

        const keys = Object.keys(data.DetectedProducts);
        for (let index = 0; index < keys.length; index++) {
          const value = data.DetectedProducts[keys[index]];
          labelsMap.push(keys[index]);
          dataArray.push(value);          
        }
        this.productNameLabels = labelsMap;
        this.productNameData = dataArray;   
      }
    });

    await this._hubConnection.start();
    await this._hubConnection.invoke("RefreshReports");
  }

}
