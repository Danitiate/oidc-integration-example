import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JWTTokenDisplayComponent } from './components/jwt-token-display.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [ 
    JWTTokenDisplayComponent
],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JWTTokenDisplayComponent
  ]
})
export class SharedModule {}