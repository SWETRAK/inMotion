import { Component } from '@angular/core';

@Component({
  selector: 'app-friends-content',
  templateUrl: './friends-content.component.html',
  styleUrls: ['./friends-content.component.css']
})
export class FriendsContentComponent {
  friendsTabActive: boolean = true;

  activateFriendsTab() {
    this.friendsTabActive = true;
  }

  activateRequestsTab() {
    this.friendsTabActive = false;
  }
}
