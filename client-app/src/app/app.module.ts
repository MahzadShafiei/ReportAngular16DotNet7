import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { EmployeeListComponent } from './components/employees/employee-list/employee-list.component';
import {HttpClientModule} from '@angular/common/http';
import { ChartModule } from 'primeng/chart';


@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    HttpClientModule,
    ChartModule,
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
