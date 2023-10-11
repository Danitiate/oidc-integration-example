import { NgModule } from '@angular/core';
import { JWTGeneratorComponent } from './page/jwt-generator.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [ 
    JWTGeneratorComponent
],
  imports: [
    SharedModule
],
})
export class JwtGeneratorModule {}