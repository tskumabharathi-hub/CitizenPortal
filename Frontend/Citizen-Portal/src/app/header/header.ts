import { Component,EventEmitter, Output } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { OnInit } from '@angular/core';
import { UserProfile } from '../models/user-profile';
import { ChangeDetectorRef } from '@angular/core';
import { UserService } from '../services/user';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})

export class Header implements OnInit{

  userName:string = 'CITIZEN';
  userProfile!: UserProfile;
  @Output() profileClick = new EventEmitter<void>();

  constructor(private userService: UserService,private cdr:ChangeDetectorRef)
  {

  }

  openProfile(){
    this.profileClick.emit();
  }

  ngOnInit(): void 
  {
    this.loadProfile();
  }

  loadProfile(): void 
  {
    this.userService.getProfile().subscribe({
      next: (response) => {
        console.log(response);
        this.userProfile = response;       
        this.userName = this.userProfile.firstName?this.userProfile.firstName:this.userProfile.email;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.log(error);
        this.cdr.detectChanges();
      }
    });
  }

}