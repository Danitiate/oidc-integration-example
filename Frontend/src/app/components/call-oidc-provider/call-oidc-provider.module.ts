import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CallOIDCProviderRoutingModule } from './call-oidc-provider.module.routing.module';
import { CallbackComponent } from './components/callback.component';
import { CallOIDCProviderComponent } from './page/call-oidc-provider.component';

@NgModule({
  declarations: [
    CallbackComponent,
    CallOIDCProviderComponent
  ],
  imports: [
    CallOIDCProviderRoutingModule,
    SharedModule
  ],
  exports: [CallOIDCProviderRoutingModule]
})
export class CallOIDCProviderModule {}