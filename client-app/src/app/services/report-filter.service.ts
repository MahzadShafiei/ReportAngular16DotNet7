import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {tagValueModel} from 'src/app/models/tagValueModel';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportFilterService {

  baseURLApi: string= environment.apiUrl;

  constructor(private http:HttpClient) { }

  getTagValueByFilter():Observable<tagValueModel[]>
  {
    return this.http.get<tagValueModel[]>(this.baseURLApi + '/api/TagValue/GetByFilter');    
  }
  
}
