import { Component } from '@angular/core';

@Component({
  selector: 'app-post-content',
  templateUrl: './post-content.component.html',
  styleUrls: ['./post-content.component.css']
})
export class PostContentComponent {

  commentsVisible: boolean = false;
  toggleComments() {
    this.commentsVisible = !this.commentsVisible;
  }
  heartCount: number = 0;
  wowCount: number = 0;
  likeCount: number = 0;
  cryCount: number = 0;
  lolCount: number = 0;

  react(reaction: string) {
    switch (reaction) {
      case 'heart':
        this.heartCount++;
        break;
      case 'wow':
        this.wowCount++;
        break;
      case 'like':
        this.likeCount++;
        break;
      case 'cry':
        this.cryCount++;
        break;
      case 'lol':
        this.lolCount++;
        break;
      default:
        break;
    }
  }

}
