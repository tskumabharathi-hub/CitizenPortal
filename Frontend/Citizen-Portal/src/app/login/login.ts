import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule, NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-login',
  imports: [FormsModule, NgIf],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login 
{

  email:string = "";
  password:string = "";
  loginError:string = '';
  emailOrPhonePattern = 
  '^((\\+\\d{1,3}[- ]?)?\\d{10}$)|(^[\\w.%+-]+@[\\w.-]+\\.[a-zA-Z]{2,}$)';

  private loginUrl = "http://localhost:5298/api/Auth/Login";

  constructor(private http: HttpClient,private router:Router,private cdr: ChangeDetectorRef)
  {

  }

  login()
  {
    this.loginError = '';

    if (!this.email || this.email.trim().length === 0) {
      this.loginError = 'Email is required';
      return;
    }

     if (!this.password || this.password.trim().length === 0) {
      this.loginError = 'Password is required';
      return;
    }

    const request = {
      email: this.email,
      password: this.password
    };

    this.http.post(this.loginUrl, request).subscribe({
      next: (response: any) => {

        if (response.isSuccess) 
        {
          localStorage.setItem("token", response.token);
          localStorage.setItem("tokenExpiration", response.expiration);
          console.log("Token Saved");
          // Navigate to Dashboard
          this.router.navigate(['/dashboard']);
        }

      },
      error: (error) => {
        console.log(error);
        this.loginError = error.error.message;
        console.log(this.loginError);
        this.cdr.detectChanges();   
      }
    });

  }

}
