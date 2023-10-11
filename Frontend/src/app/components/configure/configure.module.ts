import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ConfigureRoutingModule } from './configure.routing.module';
import { ConfigureComponent } from './page/configure.component';

@NgModule({
    declarations: [
        ConfigureComponent
],
    imports: [
        ConfigureRoutingModule,
        SharedModule
],
    exports: [ConfigureRoutingModule]
})
export class ConfigureModule { }