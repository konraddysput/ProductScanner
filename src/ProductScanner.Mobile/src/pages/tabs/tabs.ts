import { Component } from '@angular/core';

import { ContactPage } from '../contact/contact';
import { HomePage } from '../home/home';
import { HistoryPage } from '../history/history';
import { ReportPage } from '../report/report';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {

  tab1Root = HomePage;
  tab2Root = HistoryPage;
  tab3Root = ReportPage;
  tab4Root = ContactPage;
  

  constructor() {

  }
}
