import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-profile-page-followers',
  templateUrl: './profile-page-followers.component.html',
  styleUrls: ['./profile-page-followers.component.css']
})
export class ProfilePageFollowersComponent {
  constructor(
    public dialogRef: MatDialogRef<ProfilePageFollowersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }
  closeAddFriendModal() {
    this.dialogRef.close();
  }
  followersTabActive: boolean = true;
  activateFollowersTab() {
    this.followersTabActive = true;
  }
  activateFollowingTab() {
    this.followersTabActive = false;
  }
}
