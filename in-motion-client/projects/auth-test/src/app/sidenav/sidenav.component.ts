import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})

export class SidenavComponent {
  currentPage: string = '';
  constructor(private router: Router) { }

  navigateToHomePage() {
    this.router.navigate(['/home']);
    this.currentPage = 'home';
  }

  navigateToFriendsPage() {
    this.router.navigate(['/friends']);
    this.currentPage = 'friends';
  }

  navigateToProfilePage() {
    this.router.navigate(['/profile']);
    this.currentPage = 'profile';
  }
  isExpanded: boolean = false;

  expandMenu() {
    this.isExpanded = true;
  }

  collapseMenu() {
    this.isExpanded = false;
  }
}
