import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CallbackComponent } from './components/callback.component';
import { CallOIDCProviderComponent } from './page/call-oidc-provider.component';

@NgModule({
  declarations: [
    CallbackComponent,
    CallOIDCProviderComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
})
export class CallOIDCProviderModule {}