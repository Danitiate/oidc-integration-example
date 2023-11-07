import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ConfigureRoutingModule } from './configure.routing.module';
import { ConfigureComponent } from './page/configure.component';
import { TableModule } from 'primeng/table';

@NgModule({
    declarations: [
        ConfigureComponent
],
    imports: [
        ConfigureRoutingModule,
        SharedModule,
        TableModule
],
    exports: [ConfigureRoutingModule]
})
export class ConfigureModule { }