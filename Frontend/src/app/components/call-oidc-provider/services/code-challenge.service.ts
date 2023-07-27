import { Injectable } from "@angular/core";
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CodeChallengeService {
  constructor() {}

  // Reference: https://datatracker.ietf.org/doc/html/rfc7636
  generateCodeVerifier() {
    const random = CryptoJS.lib.WordArray.random(128);
    const randomBase64 = this.base64URL(random);
    const codeVerifier = randomBase64.substring(0, 128); // Code verifier needs to be between 43 - 128 characters
    // I don't recommend storing the code_verifier in session storage, but it's good enough for this demo
    sessionStorage.setItem("code_verifier", codeVerifier);
    return codeVerifier;
  }
  
  generateCodeChallenge(code_verifier: string) {
    const codeChallenge = CryptoJS.SHA256(code_verifier);
    const codeChallengeBase64 = this.base64URL(codeChallenge);
    return codeChallengeBase64;
  }
  
  private base64URL(string: CryptoJS.lib.WordArray) {
    return string.toString(CryptoJS.enc.Base64).replace(/=/g, '').replace(/\+/g, '-').replace(/\//g, '_');
  }
}