import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})

export class Profile {

  isEditing = false;

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

  editProfile() {
    this.isEditing = true;
  }

  saveProfile() {
    this.isEditing = false;
    console.log(this.profile);
  }

  cancelEdit()
  {
    this.isEditing = false;
  }

}