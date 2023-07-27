import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JWTTokenDisplayComponent } from './components/jwt-token-display.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [ 
    JWTTokenDisplayComponent
],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    JWTTokenDisplayComponent
  ]
})
export class SharedModule {}