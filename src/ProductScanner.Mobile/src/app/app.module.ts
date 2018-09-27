import { NgModule, ErrorHandler } from '@angular/core';
import { HttpClientModule } from  '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';

import {Camera} from '@ionic-native/camera';
// pages
import { ContactPage } from '../pages/contact/contact';
import { HistoryPage } from '../pages/history/history'

import { HomePage } from '../pages/home/home';
import { TabsPage } from '../pages/tabs/tabs';
import { LoginPage } from "../pages/login/login";
import { PhotoDescriptionPage } from '../pages/photo-description/photo-description';

// components
import { PhotoDetailComponent } from '../components/photo-detail/photo-detail';
import { RegisterPage } from '../pages/register/register';
import { WelcomePage } from '../pages/welcome/welcome';


import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
// services
import { AuthService } from '../providers/auth-service/auth-service';
import { PhotoService } from '../providers/photo-service/photo-service';
import { HubService } from '../providers/hub-service/hub-service';
import { ApiService } from '../providers/api-service/api-service';
import { ReportPage } from '../pages/report/report';

@NgModule({
  declarations: [
    MyApp,
    WelcomePage,
    LoginPage,
    PhotoDescriptionPage,
    RegisterPage,
    HistoryPage,
    ContactPage,
    HomePage,
    TabsPage,
    ReportPage,
    PhotoDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    WelcomePage,
    LoginPage,
    PhotoDescriptionPage,
    RegisterPage,
    HistoryPage,
    ContactPage,
    ReportPage,
    HomePage,
    TabsPage
  ],
  providers: [
    ApiService,
    StatusBar,
    SplashScreen,
    Camera,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    AuthService,
    PhotoService,
    ApiService,
    HubService
  ]
})
export class AppModule {}
