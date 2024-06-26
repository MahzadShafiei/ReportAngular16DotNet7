import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { tagValueModel } from 'src/app/models/tagValueModel';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { filterParameter } from '../Dto/Exclude/FilterParameter';
import { ChartModel } from '../Dto/Include/ChartModel';
import { unitModel } from 'src/app/models/unitModel';

@Injectable({
  providedIn: 'root'
})
export class ReportFilterService {

  baseURLApi: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getTagValueByFilter(parameters: filterParameter): Observable<ChartModel[]> {

    var getByFilterParameter = new HttpParams({ fromObject: parameters as any });

    return this.http.get<ChartModel[]>(
      this.baseURLApi + '/api/TagValue/GetGraphDataByFilter', {
      params: getByFilterParameter
    }
    );
  }

  getCalculatedAssumption(parameters: filterParameter): Observable<number> {
    var getByFilterParameter = new HttpParams({ fromObject: parameters as any });

    return this.http.get<number>(
      this.baseURLApi + '/api/TagValue/GetCalculatedAssumptionByFilter', {
      params: getByFilterParameter
    }
    );
  }

  GetUnitsByParameter(parentId: number): Observable<unitModel[]> {
    return this.http.get<unitModel[]>(this.baseURLApi + '/api/TagValue/GetUnitsByParameter?parentId=' + parentId);
  }


}
