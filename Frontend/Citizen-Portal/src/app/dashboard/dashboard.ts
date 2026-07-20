import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Header } from '../header/header';
import { Sidenav } from '../sidenav/sidenav';
import { RouterOutlet } from '@angular/router';

@Component({
  imports: [FormsModule, Header, Sidenav,[RouterOutlet]],
  selector: 'app-dashboard',
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})

export class Dashboard {

  sidebar=true;

  toggleSidebar(){

    //this.sidebar=!this.sidebar;
    this.sidebar = true;
  }

}