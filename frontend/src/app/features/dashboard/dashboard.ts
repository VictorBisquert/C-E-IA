import { Component, inject, OnInit } from '@angular/core';
import { Auth } from '../../core/services/auth';
import { UserInfo } from '../auth/models/auth.models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css'],
})
export class Dashboard {
  private authService = inject(Auth);
  currentUser: UserInfo | null = null;


  //para tema ususario
  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
  }

  logout(): void {
    this.authService.logout();
  }
}
