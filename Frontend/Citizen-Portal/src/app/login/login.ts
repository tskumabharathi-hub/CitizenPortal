import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login 
{

  contactInfo:string = "";
  password:string = "";
  emailOrPhonePattern = 
  '^((\\+\\d{1,3}[- ]?)?\\d{10}$)|(^[\\w.%+-]+@[\\w.-]+\\.[a-zA-Z]{2,}$)';

  login()
  {
    
  }

}
