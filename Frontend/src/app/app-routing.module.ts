import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'configure', 
        loadChildren: () => 
          import('./components/configure/configure.module').then(m => m.ConfigureModule)
      },
      { path: 'jwt-generator',
        loadChildren: () => 
          import('./components/jwt-generator/jwt-generator.module').then(m => m.JwtGeneratorModule)
      },
      { path: 'call-oidc-provider', loadChildren: () => 
          import('./components/call-oidc-provider/call-oidc-provider.module').then(m => m.CallOIDCProviderModule) 
      }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: true,
    }),
  ],
  exports: [RouterModule],
  providers: []
})

export class AppRoutingModule { }
