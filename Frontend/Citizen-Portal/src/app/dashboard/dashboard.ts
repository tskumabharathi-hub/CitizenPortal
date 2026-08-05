import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Header } from '../header/header';
import { Sidenav } from '../sidenav/sidenav';
import { RouterOutlet } from '@angular/router';
import { UserService } from '../services/user';
import { UserProfile } from '../models/user-profile';
import { OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  imports: [FormsModule, Header, Sidenav,NgIf,[RouterOutlet]],
  selector: 'app-dashboard',
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})

export class Dashboard implements OnInit 
{

  sidebar = true;
  isLoading = true;
  profile!: UserProfile;

  constructor(private userService: UserService,private cdr:ChangeDetectorRef)
  {

  }

  ngOnInit(): void 
  {
    this.loadProfile();
  }

  loadProfile(): void 
  {

    this.isLoading = true;
    this.userService.getProfile().subscribe({
      next: (response) => {
        console.log(response);
        this.profile = response;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.log(error);
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });

  }

  toggleSidebar()
  {
    this.sidebar = true;
  }

}