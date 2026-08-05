import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user';
import { UserProfile } from '../models/user-profile';
import { ChangeDetectorRef } from '@angular/core';
import { OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, 
            FormsModule,
            MatCardModule,
            MatButtonModule,
            MatIconModule,
            MatInputModule,
            MatFormFieldModule,
            MatSelectModule,
            MatProgressSpinnerModule],
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
    fullName: '',
    state: '',
    district:''
  };

  constructor(private userService: UserService,private cdr:ChangeDetectorRef,private profileService: ProfileService)
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
        this.profile.email = this.userProfile.email;

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

  onPincodeChange() {

    if (this.profile.pincode.length !== 6)
      return;

    this.profileService
        .getLocationByPincode(this.profile.pincode)
        .subscribe(
        {
          next: (response) => 
          {
            console.log(response);
            this.profile.state = response.state;
            this.profile.district = response.district;
            this.profile.city = response.district;
            this.cdr.detectChanges();
          },
          error: err => console.error(err)
        });
  }

}