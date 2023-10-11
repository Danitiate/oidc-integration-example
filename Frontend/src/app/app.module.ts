import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { ConfigureComponent } from './components/configure/page/configure.component';
import { JWTGeneratorComponent } from './components/jwt-generator/page/jwt-generator.component';
import { CallOIDCProviderComponent } from './components/call-oidc-provider/page/call-oidc-provider.component';
import { JwtGeneratorModule } from './components/jwt-generator/jwt-generator.module';
import { CallOIDCProviderModule } from './components/call-oidc-provider/call-oidc-provider.module';
import { SharedModule } from './components/shared/shared.module';
import { CallbackComponent } from './components/call-oidc-provider/components/callback.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ConfigureComponent
  ],
  imports: [
    SharedModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'configure', component: ConfigureComponent },
      { path: 'jwt-generator', component: JWTGeneratorComponent },
      { path: 'call-oidc-provider', component: CallOIDCProviderComponent },
      { path: 'call-oidc-provider/callback', component: CallbackComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
