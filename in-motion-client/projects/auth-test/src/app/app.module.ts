import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
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
import { RegisterPageComponent } from './register-page/register-page.component';
import { RegisterTitleComponent } from './register-page/register-title/register-title.component';
import { RegisterCredentialsComponent } from './register-page/register-credentials/register-credentials.component';
import { FriendsPageComponent } from './friends-page/friends-page.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LogoComponent } from './navbar/logo/logo.component';
import { UsernameComponent } from './navbar/username/username.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { HomePageComponent } from './home-page/home-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { FriendsContentComponent } from './friends-page/friends-content/friends-content.component';
import { AddFriendContentComponent } from './friends-page/add-friend-content/add-friend-content.component';


@NgModule({
    declarations: [
        AppComponent,
        LoginPageComponent,
        LoginBannerComponent,
        LoginCredentialsComponent,
        LoginTitleComponent,
        RegisterPageComponent,
        RegisterTitleComponent,
        RegisterCredentialsComponent,
        FriendsPageComponent,
        NavbarComponent,
        LogoComponent,
        UsernameComponent,
        SidenavComponent,
        HomePageComponent,
        ProfilePageComponent,
        FriendsContentComponent,
        AddFriendContentComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        GoogleSigninButtonModule,
        HttpClientModule,
        SocialLoginModule,
        FontAwesomeModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        FormsModule
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
