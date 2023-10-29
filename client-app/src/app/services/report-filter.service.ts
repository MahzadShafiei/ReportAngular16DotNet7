import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {tagValueModel} from 'src/app/models/tagValueModel';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { filterParameter } from '../Dto/Exclude/FilterParameter';

@Injectable({
  providedIn: 'root'
})
export class ReportFilterService {

  baseURLApi: string= environment.apiUrl;

  constructor(private http:HttpClient) { }

  getTagValueByFilter(parameters:filterParameter):Observable<tagValueModel[]>
  {

    var getByFilterParameter= new HttpParams ({fromObject:parameters as any});

    return this.http.get<tagValueModel[]>(
      this.baseURLApi + '/api/TagValue/GetByFilter', {
        params: getByFilterParameter
      }
    );    
  }
  
}
