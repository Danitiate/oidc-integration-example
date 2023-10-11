import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CallOIDCProviderComponent } from './page/call-oidc-provider.component';
import { CallbackComponent } from './components/callback.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '', component: CallOIDCProviderComponent
            },
            {
                path: 'callback', component: CallbackComponent
            }
        ])
    ],
    exports: [RouterModule]
})

export class CallOIDCProviderRoutingModule { }