import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '../models/loginRequest';
import { LoginResponse } from '../models/LoginResponse';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private apiUrl = 'http://localhost:5298/api/Auth';

  constructor(private http: HttpClient) { }

  login(request: LoginRequest): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(
      `${this.apiUrl}/Login`,
      request
    );

  }

}