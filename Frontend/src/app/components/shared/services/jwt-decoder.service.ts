import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { JWTResponse } from "../../jwt-generator/models/JWTResponse";


@Injectable({
  providedIn: 'root'
})
export class JWTDecoderService {
  constructor(private http: HttpClient) {}

  decodeJWT(token: string): Observable<JWTResponse> {
    return this.http.post<JWTResponse>(`${environment.apiBaseUrl}/JWTGenerator/Decode`, { token: token });
  }
}