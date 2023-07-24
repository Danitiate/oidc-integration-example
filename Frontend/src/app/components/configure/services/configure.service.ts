import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OIDCConfiguration } from '../models/OIDCConfiguration';
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ConfigureService {
  constructor(private http: HttpClient) {}

  getConfigurations(): Observable<OIDCConfiguration[]> {
    return this.http.get<OIDCConfiguration[]>(`${environment.apiBaseUrl}/Configuration/GetAllConfigurations`);
  }

  saveConfiguration(configuration: OIDCConfiguration): Observable<OIDCConfiguration> {
    return this.http.post<OIDCConfiguration>(`${environment.apiBaseUrl}/Configuration/SaveConfiguration`, configuration);
  }

  deleteConfiguration(configuration: OIDCConfiguration): Observable<OIDCConfiguration> {
    return this.http.delete<OIDCConfiguration>(`${environment.apiBaseUrl}/Configuration/DeleteConfiguration`, { body: configuration });
  }
}