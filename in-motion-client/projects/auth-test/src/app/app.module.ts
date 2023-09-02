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
import { HttpClientModule } from "@angular/common/http";
import { LoginPageComponent } from './login-page/login-page.component';
import { LoginBannerComponent } from './login-page/login-banner/login-banner.component';
import { LoginCredentialsComponent } from './login-page/login-credentials/login-credentials.component';
import { LoginTitleComponent } from './login-page/login-title/login-title.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [
        AppComponent,
        LoginPageComponent,
        LoginBannerComponent,
        LoginCredentialsComponent,
        LoginTitleComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        GoogleSigninButtonModule,
        HttpClientModule,
        SocialLoginModule,
        FontAwesomeModule,
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
