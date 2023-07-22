import {Component, OnDestroy, OnInit} from '@angular/core';
import {FacebookLoginProvider, GoogleLoginProvider, SocialAuthService} from "@abacritt/angularx-social-login";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit , OnDestroy{

    private accessToken = '';

    constructor(
        private authService: SocialAuthService,
        private httpClient: HttpClient
    ){

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


}
