import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
    GoogleLoginProvider,
    GoogleSigninButtonModule,
    SocialAuthServiceConfig,
    SocialLoginModule
} from "@abacritt/angularx-social-login";
import {HttpClientModule} from "@angular/common/http";

@NgModule({
  declarations: [
    AppComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        GoogleSigninButtonModule,
        HttpClientModule,
        SocialLoginModule
    ],
  providers: [
      {
          provide: 'SocialAuthServiceConfig',
          useValue: {
              autoLogin: false,
              providers: [
                  {
                      id: GoogleLoginProvider.PROVIDER_ID,
                      provider: new GoogleLoginProvider(
                          '189597209032-ca0gmusn1k9b430o1le7g6homn2tn7hm.apps.googleusercontent.com'
                      )
                  },
                  // {
                  //     id: FacebookLoginProvider.PROVIDER_ID,
                  //     provider: new FacebookLoginProvider('clientId')
                  // }
              ],
              onError: (error) => {
                  console.error(error);
              }
          } as SocialAuthServiceConfig,
      }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
