import { Component, Input } from '@angular/core';

@Component({
  selector: 'jwt-token-display',
  templateUrl: './jwt-token-display.component.html'
})
export class JWTTokenDisplayComponent {
  @Input()
  JWTstring = "";
  @Input()
  JWTheader = "";
  @Input()
  JWTpayload = "";

  constructor() {    
  }

  copyToClipboard() {
    if (navigator.clipboard) {
      navigator.clipboard.writeText(this.JWTstring).then(
        () => {
          console.log('Copied token to clipboard.');
        },
        (err) => {
          console.error('Could not copy token to clipboard: ', err);
        }
      );
    }
  }
}
