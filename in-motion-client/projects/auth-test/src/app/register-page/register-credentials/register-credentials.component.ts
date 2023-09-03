import { Component } from '@angular/core';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-register-credentials',
  templateUrl: './register-credentials.component.html',
  styleUrls: ['./register-credentials.component.css']
})
export class RegisterCredentialsComponent {
  public passwordVisible: boolean = false;
  public repeatpasswordVisible: boolean = false;

  faEye = faEye;
  faEyeSlash = faEyeSlash;

  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible;
  }

  toggleRepeatPasswordVisibility(): void {
    this.repeatpasswordVisible = !this.repeatpasswordVisible;
  }
}