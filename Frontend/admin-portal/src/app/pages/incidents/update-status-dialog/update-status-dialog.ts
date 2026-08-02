import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';

import { FormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { HttpClient } from '@angular/common/http';
import { Incident } from '../../../core/models/incident';
import { IncidentService } from '../../../core/services/incident';

@Component({
  selector: 'app-update-status-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './update-status-dialog.html',
  styleUrls: ['./update-status-dialog.scss']
})
export class UpdateStatusDialogComponent implements OnInit {

  status = '';
  remarks = '';
  assignedTo = '';

  constructor(
    public dialogRef: MatDialogRef<UpdateStatusDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public incident: Incident,
    private http:HttpClient,
    private cdr:ChangeDetectorRef,
    private incidentService:IncidentService) 
  {
    console.log(incident);
    this.status = incident.status;
  }


  ngOnInit(): void 
  {
    console.log(this.incident);
    this.cdr.detectChanges();
  }

  save() 
  {

    const body = {
      status: this.status,
      remarks: this.remarks
    };

    this.incidentService.updateComplaintStatus(this.incident.complaintId, body).subscribe(
    {
      next: (response) => {
        console.log(response);
        this.dialogRef.close(true);
      },
      error: (error) => {
        console.error(error);
      }
    });

  }

  cancel() 
  {
    this.dialogRef.close();
  }

}