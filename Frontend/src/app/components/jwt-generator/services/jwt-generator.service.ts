import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { OIDCConfiguration } from "../../configure/models/OIDCConfiguration";
import { JWTResponse } from "../models/JWTResponse";

@Injectable({
  providedIn: 'root'
})
export class JWTGeneratorService {
  constructor(private http: HttpClient) {}

  createJWTFromConfiguration(configuration: OIDCConfiguration): Observable<JWTResponse> {
    return this.http.post<JWTResponse>(`${environment.apiBaseUrl}/JWTGenerator/Create`, configuration);
  }
}