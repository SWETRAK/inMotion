import {Component, OnDestroy, OnInit} from '@angular/core';
import {GoogleLoginProvider, SocialAuthService} from "@abacritt/angularx-social-login";
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
        });
    }

    getAccessToken(): void {
        this.authService.signIn(GoogleLoginProvider.PROVIDER_ID).then(a => {
            console.log(a);
        })
    }

    getGoogleCalendarData(): void {
        if (!this.accessToken) return;

        this.httpClient
            .get('https://www.googleapis.com/calendar/v3/calendars/primary/events', {
                headers: { Authorization: `Bearer ${this.accessToken}` },
            })
            .subscribe((events) => {
                alert('Look at your console');
                console.log('events', events);
            });
    }
}
