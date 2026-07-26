import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import { ElementRef, QueryList, ViewChildren } from '@angular/core';

@Component({
  selector: 'app-signup',
  imports: [FormsModule,RouterLink,CommonModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css',
  standalone:true
})
export class Signup 
{
  @ViewChildren('otpInput')
  otpInputs!: QueryList<ElementRef<HTMLInputElement>>;
  
  email:string = '';
  password:string = '';
  reTypePassword:string = '';
  otp: string[] = ['', '', '', '', ''];


  otpError: string = '';
  passwordError:string = '';
  retypePasswordError:string = '';
  emailError:String = '';

  showSignupScreen:boolean = true;
  showOtpScreen:boolean = false;
  isOtpSuccess:boolean = false;

  private registerUrl = "http://localhost:5298/api/Auth/Register";
  private verifyOtpUrl = "http://localhost:5298/api/Auth/VerifyOtp";

  constructor(private http: HttpClient,private cdr: ChangeDetectorRef)
  {

  }

  validateForm(): boolean {
    let isValid = true;

    this.retypePasswordError = '';
    this.passwordError = '';
    this.emailError = ''

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

    // Retype Password validation
    if (!this.reTypePassword.trim()) {
      this.retypePasswordError = 'Retype password is required';
      isValid = false;
    } else if (this.password !== this.reTypePassword) {
      this.retypePasswordError = 'Passwords do not match';
      isValid = false;
    }

    return isValid;
  }

  createAccount()
  {
    if (!this.validateForm()) {
      return;
    }
    
    console.log('Form Validation success');
    
    const request = {
      email: this.email,
      password: this.password
    };

    this.http.post(this.registerUrl, request).subscribe({
      next: (response: any) => {

        console.log("Registration Success", response);

        // Show OTP screen
        this.showSignupScreen = false;
        this.showOtpScreen = true;
        this.cdr.detectChanges();
      },
      error: (error) => {

        console.log(error);

        if (error.status === 400) {
          alert(error.error.message);
        }
        else {
          alert("Something went wrong.");
        }

      }
    });
  }

  getOtp(): string {
    return this.otp.join('');
  }

  verifyOtp(): void {
    this.otpError = '';
    const enteredOtp = this.getOtp();
    if (enteredOtp.length !== 5) {
      this.otpError = 'Please enter the 5-digit OTP.';
      return;
    }
    console.log('OTP:', enteredOtp);
    // Call Verify OTP API

    const request = {
      email: this.email,
      otp: enteredOtp
    };

    this.http.post(this.verifyOtpUrl, request).subscribe({
      next: (response: any) => {
        console.log('OTP Verified succesfully');
        this.showOtpScreen = false;
        this.isOtpSuccess = true;    
        this.cdr.detectChanges();   
      },
      error: (error) => {

        this.otpError = error.message;
        console.log(error);

        if (error.status === 400) {
          alert(error.error.message);
        }
        else {
          alert("Something went wrong.");
        }

      }
    });
  }

  resendOtp():void
  {

  }
  
  onOtpInput(event: Event, index: number): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/\D/g, '').substring(0, 1);
    this.otp[index] = input.value;

    if (input.value && index < this.otp.length - 1) {
      this.otpInputs.get(index+1)?.nativeElement.focus();
    }
  }

  trackByIndex(index: number): number {
    return index;
  }

  onKeyDown(event: KeyboardEvent, index: number): void {

    if (event.key === 'Backspace') {

      const input = event.target as HTMLInputElement;

      if (input.value === '' && index > 0) {
        this.otpInputs.get(index - 1)?.nativeElement.focus();
      }
    }
  }
}
