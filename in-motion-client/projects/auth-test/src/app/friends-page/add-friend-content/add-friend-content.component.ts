import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-friend-content',
  templateUrl: './add-friend-content.component.html',
  styleUrls: ['./add-friend-content.component.css']
})
export class AddFriendContentComponent {
  isFieldSelected: string | false = false;
  selectedUsername: string = '';
  requestSent: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<AddFriendContentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  closeAddFriendModal() {
    this.dialogRef.close();
  }
  selectField(field: string) {
    this.isFieldSelected = field as string;
    if (field === 'image') {
      this.selectedUsername = '';
    }
  }

  sendRequest() {
    this.requestSent = true;
    setTimeout(() => {
      this.requestSent = false;
    }, 2000);

  }
}
