import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUserWithEmailAndPasswordDto } from '../interfaces/register-user-with-email-and-password-dto';
import { SuccessfulRegistrationResponseDto } from '../interfaces/successful-registration-response-dto';
import { ImsHttpMessage } from '../interfaces/ims-http-message';
import { LoginUserWithEmailAndPasswordDto } from '../interfaces/login-user-with-email-and-password-dto';
import { UserInfoDto } from '../interfaces/user-info-dto';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private baseUrl: string = "http://localhost:8001/api/email/";

  constructor(private http: HttpClient) { }

  register(userObj: RegisterUserWithEmailAndPasswordDto) {
    return this.http.post<ImsHttpMessage<SuccessfulRegistrationResponseDto>>(`${this.baseUrl}register`, userObj);
  }

  login(loginObj: LoginUserWithEmailAndPasswordDto) {
    return this.http.post<ImsHttpMessage<UserInfoDto>>(`${this.baseUrl}login`, loginObj);
  }
}
