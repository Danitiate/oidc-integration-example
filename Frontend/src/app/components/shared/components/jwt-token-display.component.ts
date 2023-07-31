import { Component, Input } from '@angular/core';
import { JWTDecoderService } from '../services/jwt-decoder.service';

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

  constructor(private JWTDecoderService: JWTDecoderService) {    
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

  decode() {
    if (this.JWTstring) {
      this.JWTDecoderService.decodeJWT(this.JWTstring).subscribe(result => {
        console.log(result);
        this.JWTheader = JSON.stringify(result.headers, null, 2);
        this.JWTpayload = JSON.stringify(result.payload, null, 2);
      })
    } else {
      console.log("No JWT String present");
    }
  }
}
