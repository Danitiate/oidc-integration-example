import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ConfigureComponent } from './page/configure.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '', component: ConfigureComponent
            }
        ])
    ],
    exports: [RouterModule]
})

export class ConfigureRoutingModule { }