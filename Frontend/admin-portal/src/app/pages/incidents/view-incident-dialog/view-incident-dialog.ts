import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA,MatDialogModule,MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { HttpClient } from '@angular/common/http';
import { IncidentService } from '../../../core/services/incident';
import { Incident } from '../../../core/models/incident';

@Component({
  selector: 'app-view-incident-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './view-incident-dialog.html',
  styleUrls: ['./view-incident-dialog.scss']
})
export class ViewIncidentDialogComponent implements OnInit{

  incident!: Incident;
  loading = true;

  constructor(
    public dialogRef: MatDialogRef<ViewIncidentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private http:HttpClient,
    private incidentService: IncidentService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void 
  {
    console.log('Dialog data:\n');
    console.log(JSON.stringify(this.data));
    this.loadIncident();
  }

  loadIncident(): void 
  {
    console.log(this.data);
    this.incidentService.getIncidentById(this.data.complaintId).subscribe(
    {
      next: response => {
        this.incident = response;
        this.incident.imagePath = `http://localhost:5298${response.imagePath}`;
        console.log(this.incident.imagePath);
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: error => {
        console.error(error);
        this.loading = false;
      }
    });
  }

  closeDialog() 
  {
    this.dialogRef.close();
  }

  updateStatus() 
  {
    this.dialogRef.close(this.incident);
  }

  viewOnMap(): void 
  {
    if (!this.incident?.latitude || !this.incident?.longitude) 
    {
      alert('Location not available');
      return;
    }
    const latitude = this.incident.latitude;
    const longitude = this.incident.longitude;
    const url = `https://www.google.com/maps?q=${latitude},${longitude}`;
    window.open(url, '_blank');
  }
}