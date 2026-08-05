import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService 
{
  private apiUrl = 'http://localhost:5298/api/Dashboard/dashboard';
  constructor(private http: HttpClient) { }

  getDashboard() 
  {    
    return this.http.get<any>(this.apiUrl);
  }

}