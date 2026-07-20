import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-signup',
  imports: [FormsModule,RouterLink,CommonModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css',
})
export class Signup 
{
  name:string = '';
  email:string = '';
  password:string = '';

  passwordError:string = '';
  nameError:string = '';
  emailError:String = '';

  validateForm(): boolean {
    let isValid = true;

    // Reset errors
    this.nameError = '';
    this.passwordError = '';
    this.emailError = ''

    // Name validation
    if (!this.name || this.name.trim().length === 0) {
      this.nameError = 'User name is required';
      isValid = false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!this.email.trim()) {
      this.emailError = 'Email is required';
      isValid = false;
    } else if (!emailRegex.test(this.email)) {
      this.emailError = 'Enter a valid email';
      isValid = false;
    }

    // Password validation
    const passwordRegex = /^(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{8,}$/;

    if (!this.password || this.password.trim().length === 0) {
      this.passwordError = 'Password is required';
      isValid = false;
    }
    else if (!passwordRegex.test(this.password)) {
      this.passwordError =
        'Password must be 8+ chars, 1 number & 1 special symbol';
      isValid = false;
    }

    return isValid;
  }

  createAccount()
  {
    if (!this.validateForm()) {
      return;
    }
    console.log("Name = "+this.name+"\n");
    console.log("Email = "+this.email+"\n");
    console.log("Email = "+this.password+"\n");
  }

  googleLogin()
  {
    if (!this.validateForm()) {
      return;
    }
    console.log("Google login clicked!");
  }

}
