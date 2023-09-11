import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})

export class SidenavComponent {
  currentPage: string = '';
  constructor(private router: Router) {}

  navigateToHomePage() {
    this.router.navigate(['/home-page']);
    this.currentPage = 'home-page';
  }

  navigateToFriendsPage() {
    this.router.navigate(['/friends-page']);
    this.currentPage = 'friends-page';
  }

  navigateToProfilePage() {
    this.router.navigate(['/profile-page']);
    this.currentPage = 'profile-page';
  }
  isExpanded: boolean = false;

  expandMenu() {
    this.isExpanded = true;
  }

  collapseMenu() {
    this.isExpanded = false;
  }
}
