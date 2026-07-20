import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-report-issue',
  imports: [FormsModule,CommonModule],
  templateUrl: './report-issue.html',
  styleUrl: './report-issue.css',
})

export class ReportIssue 
{
  showOverlay:boolean = false;
  showAck:boolean = false;

  incidentId:string= 'CM-2024-15879';
  submittedDate:string = '29th April 2026';
  location:string = 'Chennai TN';
  status:string = 'Under Review';

  openOverlay() {
    this.showOverlay = true;
  }

  closeOverlay() {
    this.showOverlay = false;
  }
  
  submitReport() {

    this.showOverlay = false;

    this.showAck = true;

  }

  closeAcknowledgement() {

    this.showAck = false;

  }

  backHome()
  {
    this.showAck = false;
  }
}
