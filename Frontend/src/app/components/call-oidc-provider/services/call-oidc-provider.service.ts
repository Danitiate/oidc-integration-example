import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";
import { OIDCConfiguration } from "../../configure/models/OIDCConfiguration";

@Injectable({
  providedIn: 'root'
})
export class CallOIDCProviderService {
  constructor(private http: HttpClient) {}

  callAuthorizeUri(uri: string): void {
    window.location.href = uri;
  }

  callTokenUri(token: string, code: string, configuration: OIDCConfiguration): Observable<any> {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/x-www-form-urlencoded'
    );

    const body = new URLSearchParams();
    body.set("client_id", configuration.client_id!);
    body.set("redirectUri", configuration.redirect_uri!);
    body.set("grant_type", "authorization_code");
    body.set("code", code);
    body.set("code_verifier", "tbd");
    body.set("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
    body.set("client_assertion", token);

    return this.http.post<any>(configuration.audience!, body.toString(), { headers: headers });
  }
}