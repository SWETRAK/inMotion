import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddFriendContentComponent } from '../add-friend-content/add-friend-content.component';

@Component({
  selector: 'app-friends-content',
  templateUrl: './friends-content.component.html',
  styleUrls: ['./friends-content.component.css']
})
export class FriendsContentComponent {
  friendsTabActive: boolean = true;
  isOpen: boolean = false;
  activateFriendsTab() {
    this.friendsTabActive = true;
  }

  activateRequestsTab() {
    this.friendsTabActive = false;
  }
  constructor(public dialog: MatDialog) { }

  openAddFriendModal() {
    const dialogRef = this.dialog.open(AddFriendContentComponent, {

    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }
  closeAddFriendModal() {
    this.isOpen = false;
  }
}
