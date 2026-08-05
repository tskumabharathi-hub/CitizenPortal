import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Incident } from '../../core/models/incident';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ViewIncidentDialogComponent } from './view-incident-dialog/view-incident-dialog';
import { UpdateStatusDialogComponent } from './update-status-dialog/update-status-dialog';
import { IncidentService } from '../../core/services/incident';

@Component({
  selector: 'app-incidents',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatChipsModule,
    MatTooltipModule
  ],
  templateUrl: './incidents.html',
  styleUrls: ['./incidents.scss']
})

export class IncidentsComponent implements OnInit,AfterViewInit 
{
  displayedColumns = [
    'complaintId',
    'department',
    'complaintCategory',
    'status',
    'createdDate',
    'updatedDate',
    'action'
  ];

  dataSource = new MatTableDataSource<Incident>();

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  @ViewChild(MatSort)
  sort!: MatSort;

  constructor(private dialog: MatDialog,private incidentService: IncidentService) 
  {
    console.log('IncidentsComponent Constructor');
  } 

  ngOnInit(): void 
  {
    console.log('Loading Incidents');
    this.loadIncidents();
  }
  
  ngAfterViewInit() 
  {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) 
  {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openUpdateDialog(row: any) {
    const dialogRef = this.dialog.open(UpdateStatusDialogComponent, {
      width: '600px',
      data: row
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result)
      {
        this.loadIncidents();
      }
    });
  }

  loadIncidents(): void 
  {
    this.incidentService.getAllIncidents().subscribe(
    {
      next: (response) => {
        this.dataSource.data = response;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  viewIncident(row: Incident) 
  {
    this.dialog.open(ViewIncidentDialogComponent, {width: '900px',data: row});
  }

}