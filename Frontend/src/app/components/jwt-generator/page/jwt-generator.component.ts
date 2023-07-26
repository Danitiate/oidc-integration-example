import { Component } from '@angular/core';
import { OIDCConfiguration } from '../../configure/models/OIDCConfiguration';
import { ConfigureService } from '../../configure/services/configure.service';
import { JWTGeneratorService } from '../services/jwt-generator.service';

@Component({
  selector: 'jwtgenerator',
  templateUrl: './jwt-generator.component.html'
})
export class JWTGeneratorComponent {
  public oidcConfigurations: OIDCConfiguration[] = [];
  public selectedConfiguration: OIDCConfiguration | null = null;
  JWTstring = "";
  JWTheader = "";
  JWTpayload = "";

  constructor(
    private configureService: ConfigureService,
    private jwtGeneratorService: JWTGeneratorService
  ) {
    configureService.getConfigurations().subscribe(result => {
      this.oidcConfigurations = result;
    });
  }

  createJWT() {
    if (this.selectedConfiguration) {
      this.jwtGeneratorService.createJWTFromConfiguration(this.selectedConfiguration).subscribe(result => {
        this.JWTstring = result.token;
        this.JWTheader = JSON.stringify(result.headers, null, 2);
        this.JWTpayload = JSON.stringify(result.payload, null, 2);
      });
    }
  }

  copyToClipboard() {
    if (navigator.clipboard) {
      navigator.clipboard.writeText(this.JWTstring).then(
        () => {
          console.log('Copied token to clipboard.');
        },
        (err) => {
          console.error('Could not copy token to clipboard: ', err);
        }
      );
    }
  }
}