import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";
import { OIDCConfiguration } from "../../configure/models/OIDCConfiguration";
import { CodeChallengeService } from "./code-challenge.service";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CallOIDCProviderService {
  constructor(
    private http: HttpClient,
    private codeChallengeService: CodeChallengeService
  ) {}

  /**
   * Proof of concept, should not be used in production as code_verifier gets stored locally in the browser session.
   */
  callAuthorizeUriFromClient(uri: string): void {
    const codeVerifier = this.codeChallengeService.generateCodeVerifier();
    const codeChallenge = this.codeChallengeService.generateCodeChallenge(codeVerifier);
    const codeChallengeMethod = "S256";
    const responseMode = "query";
    uri += `&code_challenge=${codeChallenge}&code_challenge_method=${codeChallengeMethod}&response_mode=${responseMode}`;
    window.location.href = uri;
  }

  callAuthorizeUriFromBackend(configuration: OIDCConfiguration): Observable<any> {
    return this.http.post<any>(`${environment.apiBaseUrl}/OpenIDConnect/CallAuthorizeEndpoint`, configuration);
  }

  callTokenUri(token: string, code: string, code_verifier: string, configuration: OIDCConfiguration): Observable<any> {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/x-www-form-urlencoded'
    );

    const body = new URLSearchParams();
    body.set("client_id", configuration.client_id!);
    body.set("redirectUri", configuration.redirect_uri!);
    body.set("grant_type", "authorization_code");
    body.set("code", code);
    body.set("code_verifier", code_verifier);
    body.set("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer");
    body.set("client_assertion", token);

    return this.http.post<any>(configuration.audience!, body.toString(), { headers: headers });
  }
}