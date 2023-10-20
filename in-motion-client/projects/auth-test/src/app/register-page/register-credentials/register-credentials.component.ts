import { Component } from '@angular/core';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-credentials',
  templateUrl: './register-credentials.component.html',
  styleUrls: ['./register-credentials.component.css']
})
export class RegisterCredentialsComponent {

  constructor(private router: Router) { }

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
  redirectToLogin() {
    this.router.navigate(['/']);
  }
}