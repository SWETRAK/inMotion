import { Component, ElementRef, ViewChild } from '@angular/core';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import ValidateForm from '../../validation/validationform';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-credentials',
  templateUrl: './register-credentials.component.html',
  styleUrls: ['./register-credentials.component.css']
})

export class RegisterCredentialsComponent {

  constructor(private router: Router, private fb: FormBuilder, private auth: AuthService) { }

  @ViewChild('bootstrapAlert') bootstrapAlert!: ElementRef;
  registerForm!: FormGroup;
  private subscriptions: any[] = [];
  public passwordVisible: boolean = false;
  public repeatpasswordVisible: boolean = false;
  faEye = faEye;
  faEyeSlash = faEyeSlash;

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      nickname: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      repeatPassword: ['', Validators.required],
    })
  }

  onRegister() {
    if (this.registerForm.valid) {
      console.log("ok");
      const subscription = this.auth.register(this.registerForm.value)
        .subscribe({
          next: (res => {
            this.showBootstrapAlert('Please, confirm your account via email', 'alert-success');
            this.registerForm.reset();
            setTimeout(() => {
              this.router.navigate(['/']);
            }, 2000);
          }),
          error: (err => {
            this.showBootstrapAlert('Passwords are not the same!', 'alert-danger');
          })
        });
      this.subscriptions.push(subscription);
    }
    else {
      ValidateForm.validateAllFormFields(this.registerForm)
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

  toggleRepeatPasswordVisibility(): void {
    this.repeatpasswordVisible = !this.repeatpasswordVisible;
  }

  redirectToLogin() {
    this.router.navigate(['/']);
  }
}