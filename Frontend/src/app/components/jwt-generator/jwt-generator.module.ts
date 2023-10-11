import { NgModule } from '@angular/core';
import { JWTGeneratorComponent } from './page/jwt-generator.component';
import { SharedModule } from '../shared/shared.module';
import { JwtGeneratorRoutingModule } from './jwt-generator.routing.module';

@NgModule({
  declarations: [ 
    JWTGeneratorComponent
],
  imports: [
    JwtGeneratorRoutingModule,
    SharedModule
],
  exports: [JwtGeneratorRoutingModule]
})
export class JwtGeneratorModule {}