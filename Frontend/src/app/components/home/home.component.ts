import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccessToken } from '../call-oidc-provider/models/access-token';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  access_token: AccessToken | undefined;

  constructor(
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      var localStorageAccessTokenJson = localStorage.getItem("access_token");
      if (params.access_token) {
        var parsedAccessToken = JSON.parse(params.access_token) as AccessToken;
        if (parsedAccessToken.access_token) {
          this.access_token = parsedAccessToken;
          localStorage.setItem("access_token", JSON.stringify(parsedAccessToken))
        }
      } else if(localStorageAccessTokenJson) {
        var parsedAccessToken = JSON.parse(localStorageAccessTokenJson) as AccessToken;
        if (parsedAccessToken.access_token) {
          this.access_token = parsedAccessToken;
        }
      }
    })
  }
}
