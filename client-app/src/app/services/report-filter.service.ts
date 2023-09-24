import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {tagValueModel} from 'src/app/models/tagValueModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportFilterService {

  constructor(private http:HttpClient) { }

  getTagValueByFilter():Observable<tagValueModel[]>
  {
    return this.http.get<tagValueModel[]>('https://localhost:7087/api/TagValue/GetByFilter');
  }
}
