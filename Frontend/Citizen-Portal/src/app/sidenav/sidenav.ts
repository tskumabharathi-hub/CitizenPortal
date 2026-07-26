import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  imports: [RouterLink],
  selector: 'app-sidenav',
  templateUrl: './sidenav.html',
  styleUrls: ['./sidenav.css']
})

export class Sidenav {


  @Input() opened=false; 
  constructor(private router:Router)
  {

  }

  logout(): void 
  {
    localStorage.removeItem('token');
    localStorage.removeItem('tokenExpiration');
    this.router.navigate(['/login']);
  }

}