import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../models/user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private profileUrl = 'http://localhost:5298/api/Auth/Profile';

  constructor(private http: HttpClient) { }

  getProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(this.profileUrl);
  }

  updateProfile(data: any) {
      return this.http.put(
        'http://localhost:5298/api/Auth/Profile',
        data);
  }
}
