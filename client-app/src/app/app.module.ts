import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { EmployeeListComponent } from './components/employees/employee-list/employee-list.component';
import {HttpClientModule} from '@angular/common/http';
import { ChartModule } from 'primeng/chart';
import { CalendarModule } from 'primeng/calendar';
import { ReportMenuComponent } from './components/repor/consumption/reportMenu/report-menu/report-menu.component';
import { PanelMenuModule } from 'primeng/panelmenu'
import { FormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    ReportMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    HttpClientModule,
    ChartModule,    
    CalendarModule,
    PanelMenuModule,
    BrowserAnimationsModule,
    FormsModule,
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
