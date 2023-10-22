import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfilePageFollowersComponent } from '../profile-page-followers/profile-page-followers.component';

@Component({
  selector: 'app-profile-page-content',
  templateUrl: './profile-page-content.component.html',
  styleUrls: ['./profile-page-content.component.css']
})
export class ProfilePageContentComponent {
  isOpen: boolean = false;
  constructor(public dialog: MatDialog) { }
  openFollowersModal() {
    const dialogRef = this.dialog.open(ProfilePageFollowersComponent, {});
    dialogRef.afterClosed().subscribe(result => {

    });
  }
  closeFollowersModal() {
    this.isOpen = false;
  }
}
