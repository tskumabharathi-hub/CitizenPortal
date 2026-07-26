import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user';
import { UserProfile } from '../models/user-profile';
import { ChangeDetectorRef } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})

export class Profile implements OnInit {

  isEditing = false;
  isLoading = true;
  userProfile!: UserProfile;
  
  profile = {
    firstName: '',
    lastName: '',
    gender: '',
    mobile: '',
    address: '',
    city: '',
    pincode: '',
    email: '',
    fullName: ''
  };

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
        this.userProfile = response;

        this.profile.firstName = this.userProfile.firstName;
        this.profile.lastName = this.userProfile.surname;
        this.profile.gender = this.userProfile.gender;
        this.profile.address = this.userProfile.address;
        this.profile.pincode = this.userProfile.pincode;
        this.profile.city = this.userProfile.city;
        this.profile.mobile = this.userProfile.surname;

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

  editProfile() {
    this.isEditing = true;
  }

  saveProfile() {    
    const request = {
      firstName: this.profile.firstName,
      surname: this.profile.lastName,
      gender: this.profile.gender,
      phoneNumber: this.profile.mobile,
      address: this.profile.address,
      city: this.profile.city,
      pincode: this.profile.pincode
    };

    this.userService.updateProfile(request).subscribe({
      next: (res: any) => {
        alert(res.message);
      },
      error: err => {
        alert("Failed to update profile.");
      }
    });
    
    this.isEditing = false;
    this.cdr.detectChanges();
  }

  cancelEdit()
  {
    this.isEditing = false;
  }

}