import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DashboardService } from '../../core/services/dashboardService';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class Dashboard implements OnInit {

  constructor(private dashboardService : DashboardService, private cdr:ChangeDetectorRef)
  {

  }

  displayedColumns = [
    'id',
    'citizen',
    'department',
    'status',
    'date',
    'action'
  ];

  incidents = [
    {
      id: 'INC001',
      citizen: 'John',
      department: 'Roads',
      status: 'Pending',
      date: '01-Aug-2026'
    },
    {
      id: 'INC002',
      citizen: 'Rahul',
      department: 'Water',
      status: 'In Progress',
      date: '01-Aug-2026'
    },
    {
      id: 'INC003',
      citizen: 'Smith',
      department: 'Sanitation',
      status: 'Resolved',
      date: '31-Jul-2026'
    }
  ];

  dashboard: any;

  ngOnInit(): void 
  {
    this.dashboardService.getDashboard().subscribe(
    {
        next: (response) => 
        {
          console.log(response);
          this.dashboard = response;
          this.cdr.detectChanges();
        },
        error: err => console.error(err)
    });
  }

}