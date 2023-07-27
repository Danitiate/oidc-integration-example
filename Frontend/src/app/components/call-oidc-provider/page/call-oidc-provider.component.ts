import { Component } from '@angular/core';
import { OIDCConfiguration } from '../../configure/models/OIDCConfiguration';
import { ConfigureService } from '../../configure/services/configure.service';
import { CallOIDCProviderService } from '../services/call-oidc-provider.service';

@Component({
  selector: 'calloidcprovider',
  templateUrl: './call-oidc-provider.component.html'
})
export class CallOIDCProviderComponent {
  public oidcConfigurations: OIDCConfiguration[] = [];
  public selectedConfiguration: OIDCConfiguration | null = null;
  public authorizeUri = "";

  constructor(
    private configureService: ConfigureService,
    private providerService: CallOIDCProviderService
  ) {
    configureService.getConfigurations().subscribe(result => {
      this.oidcConfigurations = result;
      if (result.length == 1) {
        this.selectedConfiguration = result[0];
        this.createAuthorizeUri();
      }
    });
  }

  callProvider() {
    if (this.selectedConfiguration) {
      this.providerService.callAuthorizeUri(this.authorizeUri);
    }
  }

  selectedConfigurationUpdated() {
    this.createAuthorizeUri();
  }
  
  createAuthorizeUri() {
    const state = "tbd";
    const codeChallenge = "tbd";
    const codeChallengeMethod = "S256";
    const responseMode = "query";
    this.authorizeUri = `${this.selectedConfiguration?.authority}/authorize?client_id=${this.selectedConfiguration?.client_id}&redirect_uri=${this.selectedConfiguration?.redirect_uri}&response_type=${this.selectedConfiguration?.response_type}&scope=${this.selectedConfiguration?.scope}&state=${state}&code_challenge=${codeChallenge}&code_challenge_method=${codeChallengeMethod}&response_mode=${responseMode}`;
  }
}