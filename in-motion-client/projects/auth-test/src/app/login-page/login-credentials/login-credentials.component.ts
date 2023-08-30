import { Component, OnInit, OnDestroy } from '@angular/core';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from "@abacritt/angularx-social-login";
import { HttpClient } from "@angular/common/http";
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-login-credentials',
  templateUrl: './login-credentials.component.html',
  styleUrls: ['./login-credentials.component.css']
})
export class LoginCredentialsComponent implements OnInit, OnDestroy {


  private accessToken = '';

  constructor(
    private authService: SocialAuthService,
    private httpClient: HttpClient
  ) {

  }
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.authService.authState.subscribe((user) => {
      console.log(user);
      //TODO: Here if user is not null call backend function
    });
  }
  loginWithFacebook(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }
  signOut(): void {
    this.authService.signOut();
  }

  public passwordVisible: boolean = false;
  faEye = faEye;
  faEyeSlash = faEyeSlash;

  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible;
  }
}