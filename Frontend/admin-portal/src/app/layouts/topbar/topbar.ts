import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './topbar.html',
  styleUrls: ['./topbar.scss']
})
export class TopbarComponent {

  adminName = 'Administrator';

  logout() {
    console.log('Logout');
  }

}