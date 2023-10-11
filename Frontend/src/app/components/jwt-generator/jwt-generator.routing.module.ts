import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { JWTGeneratorComponent } from './page/jwt-generator.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '', component: JWTGeneratorComponent
            }
        ])
    ],
    exports: [RouterModule]
})

export class JwtGeneratorRoutingModule { }