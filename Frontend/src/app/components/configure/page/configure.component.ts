import { Component } from '@angular/core';
import { ConfigureService } from '../services/configure.service';
import { OIDCConfiguration } from '../models/OIDCConfiguration';

@Component({
  selector: 'configure',
  templateUrl: './configure.component.html'
})
export class ConfigureComponent {
  public oidcConfigurations: OIDCConfiguration[] = [];
  
  newConfiguration: OIDCConfiguration = {
    audience: null,
    authority: null,
    callback_uri: null,
    client_id: null,
    id: null,
    redirect_uri: null,
    response_type: null,
    scope: null
  };

  constructor(
    private configureService: ConfigureService
  ) {
    configureService.getConfigurations().subscribe(result => {
      this.oidcConfigurations = result;
    });
  }

  saveConfiguration(configuration: OIDCConfiguration) {
    this.configureService.saveConfiguration(configuration).subscribe(result => {
      console.log("Successfully saved " + result.id);

      this.configureService.getConfigurations().subscribe(result => {
        this.oidcConfigurations = result;        
      });

      if (configuration.id == null) {
        this.newConfiguration.audience = null;
        this.newConfiguration.authority = null;
        this.newConfiguration.callback_uri = null;
        this.newConfiguration.client_id = null;
        this.newConfiguration.redirect_uri = null;
        this.newConfiguration.response_type = null;
        this.newConfiguration.scope = null;
      }
    });
  }

  deleteConfiguration(configuration: OIDCConfiguration) {
    this.configureService.deleteConfiguration(configuration).subscribe(result => {
      console.log("Successfully deleted " + result.id);

      this.configureService.getConfigurations().subscribe(result => {
        this.oidcConfigurations = result;
      });
    });
  }
}