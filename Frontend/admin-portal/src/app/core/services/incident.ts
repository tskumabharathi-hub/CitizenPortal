import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Incident } from '../models/incident';



@Injectable({
  providedIn: 'root'
})
export class IncidentService 
{
  private apiUrl = 'http://localhost:5298/api/Complaint';
  constructor(private http: HttpClient) { }

  getAllIncidents(): Observable<Incident[]> 
  {
    return this.http.get<Incident[]>(this.apiUrl);
  }

  getIncidentById(id: string): Observable<Incident> 
  {
    return this.http.get<Incident>(`${this.apiUrl}/${id}`);
  }

  updateComplaintStatus(id: string, request: any) 
  {
    return this.http.patch(`${this.apiUrl}/${id}/status`,request);
  }

}