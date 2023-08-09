import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {
    FacebookLoginProvider,
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
                          '435519606946-ps5v1hfb25fsh00biaam6rnf91ged72r.apps.googleusercontent.com'
                      )
                  },
                  {
                      id: FacebookLoginProvider.PROVIDER_ID,
                      provider: new FacebookLoginProvider(
                          "1046377159865274",
                          {
                          scope: 'email',
                          return_scopes: true,
                          enable_profile_selector: true
                      })
                  }
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
