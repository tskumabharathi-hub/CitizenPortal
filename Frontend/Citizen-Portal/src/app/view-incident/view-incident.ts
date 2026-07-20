import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  standalone:true,
  imports:[FormsModule,CommonModule],
  selector: 'app-view-incidents',
  templateUrl: './view-incident.html',
  styleUrls: ['./view-incident.css']
})
export class ViewIncident {

  incidents: any[] = [];

}