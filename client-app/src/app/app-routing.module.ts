import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './components/employees/employee-list/employee-list.component';
import { ReportMenuComponent } from './components/repor/consumption/reportMenu/report-menu/report-menu.component';

const routes: Routes = [
  {
    path:'employees',
    component:EmployeeListComponent
  },
  {
  path:'reportMenu',
  component:ReportMenuComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
