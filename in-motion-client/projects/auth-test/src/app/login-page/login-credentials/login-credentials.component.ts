import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from "@abacritt/angularx-social-login";
import { HttpClient } from "@angular/common/http";
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import ValidateForm from '../../validation/validateform';

@Component({
  selector: 'app-login-credentials',
  templateUrl: './login-credentials.component.html',
  styleUrls: ['./login-credentials.component.css']
})

export class LoginCredentialsComponent implements OnInit, OnDestroy {

  private accessToken = '';

  constructor(
    private authService: SocialAuthService,
    private httpClient: HttpClient,
    private router: Router,
    private auth: AuthService,
    private formBuilder: FormBuilder
  ) {

  }

  @ViewChild('bootstrapAlert') bootstrapAlert!: ElementRef;
  loginForm!: FormGroup;
  private subscriptions: any[] = [];
  public passwordVisible: boolean = false;
  eyeIcon = faEye;
  closedEyeIcon = faEyeSlash;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  loginWithFacebook(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.authService.signOut();
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      console.log("ok");
      const subscription = this.auth.login(this.loginForm.value)
        .subscribe({
          next: (res) => {
            console.log(this.loginForm.value);
            this.loginForm.reset();
            this.router.navigate(['/home']);
          },
          error: (err) => {
            this.showBootstrapAlert('Wrong e-mail or password!', 'alert-danger');
          }
        });
      this.subscriptions.push(subscription);
    }
    else {
      console.log("error");
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  showBootstrapAlert(message: string, alertType: string) {
    const alertElement: HTMLElement = this.bootstrapAlert.nativeElement;
    alertElement.innerHTML = message;
    alertElement.classList.add('alert', alertType);
    alertElement.style.display = 'block';
    setTimeout(() => {
      alertElement.style.display = 'none';
    }, 2000);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible;
  }

  redirectToRegister() {
    this.router.navigate(['/register']);
  }
}