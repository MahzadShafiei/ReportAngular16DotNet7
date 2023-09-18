import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { employeeModel } from 'src/app/models/employeeModel';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor(private http: HttpClient) { }

  getAllEmployees():Observable<employeeModel[]>
  {
    return this.http.get<employeeModel[]>('https://localhost:7087/api/Employees');
  }
}
