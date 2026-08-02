import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ComplaintResp } from '../models/ComplaintResponse';
import { OnInit } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-view-incidents',
  imports: [CommonModule],
  templateUrl: './view-incident.html',
  styleUrls: ['./view-incident.css']
})
export class ViewIncident implements OnInit 
{

  incidents: ComplaintResp[] = [];

  private apiUrl = 'http://localhost:5298/api/Complaint/my';

  constructor(private http: HttpClient,private cdr:ChangeDetectorRef) {
   
  }

  ngOnInit(): void 
  {
     this.getIncidents();
  }

  getIncidents(): void 
  {
    this.http.get<ComplaintResp[]>(this.apiUrl)
      .subscribe({
          next: (response) => {
              console.log(response);
              this.incidents = response;
              this.cdr.detectChanges();
          },
          error: (error) => 
          {
            console.error(error);
            alert('Unable to load incidents.');
          }
      });
  }

}