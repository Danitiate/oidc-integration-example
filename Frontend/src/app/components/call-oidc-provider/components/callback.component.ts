import { Component, OnInit } from '@angular/core';
import { OIDCConfiguration } from '../../configure/models/OIDCConfiguration';
import { ConfigureService } from '../../configure/services/configure.service';
import { ActivatedRoute, Router } from '@angular/router';
import { JWTGeneratorService } from '../../jwt-generator/services/jwt-generator.service';
import { CallOIDCProviderService } from '../../call-oidc-provider/services/call-oidc-provider.service';

@Component({
  selector: 'callback',
  templateUrl: './callback.component.html'
})
export class CallbackComponent implements OnInit {
  public oidcConfigurations: OIDCConfiguration[] = [];
  public selectedConfiguration: OIDCConfiguration | null = null;
  code = "";
  state = "";

  jwtStatus = "Pending";
  tokenEndpointStatus = "Pending";

  JWTstring = "";
  JWTheader = "";
  JWTpayload = "";

  constructor(
    private configureService: ConfigureService,
    private route: ActivatedRoute,
    private router: Router,
    private jwtGeneratorService: JWTGeneratorService,
    private callOIDCProviderService: CallOIDCProviderService,
  ) {
  }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        if(params.code && params.state) {
          console.log("Callback received from OIDC provider");
          this.code = params.code;
          this.state = params.state;
          this.CreateJWTAndCallTokenEndpoint();
        }
        else {
          this.router.navigate(["/call-oidc-provider"]);
        }
      }
    );
  }

  private CreateJWTAndCallTokenEndpoint() {
    this.configureService.getConfigurations().subscribe(result => {
      if (result.length == 1) {
        this.selectedConfiguration = result[0];
        this.jwtGeneratorService.createJWTFromConfiguration(this.selectedConfiguration).subscribe(result => {
          if (result.token) {
            this.jwtStatus = "OK";
            this.JWTstring = result.token;
            this.JWTheader = JSON.stringify(result.headers, null, 2);
            this.JWTpayload = JSON.stringify(result.payload, null, 2);
            this.CallTokenEndpoint();
          }
        });
      } else {
        this.jwtStatus = "Failed";
      }
    });
  }

  private CallTokenEndpoint() {
    this.callOIDCProviderService.callTokenUri(this.JWTstring, this.code, this.selectedConfiguration!).subscribe(result => {
      this.tokenEndpointStatus = "OK";
      console.log(result);
    });
  }
}