import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private apiUrl = 'http://localhost:5298/api/Profile';

  constructor(private http: HttpClient) { }

  // Get Logged-in User Profile
  getProfile(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  // Update Profile
  updateProfile(profile: any): Observable<any> {
    return this.http.put<any>(this.apiUrl, profile);
  }

  // Get State, District & City from Pincode
  getLocationByPincode(pincode: string): Observable<any> {
    return this.http.get<any>(
      `http://localhost:5298/api/Profile/pincode/${pincode}`
    );
  }

  // Upload Profile Photo (Future)
  uploadProfilePhoto(file: File): Observable<any> {

    const formData = new FormData();
    formData.append('photo', file);

    return this.http.post<any>(
      `${this.apiUrl}/upload-photo`,
      formData
    );
  }
}