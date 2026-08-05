import { AfterViewInit, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import * as L from 'leaflet';

import { MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-location-picker',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './location-picker.html',
  styleUrl: './location-picker.css'
})
export class LocationPicker implements AfterViewInit {

  map!: L.Map;

  marker!: L.Marker;

  latitude = 13.0827;

  longitude = 80.2707;

  address = '';

  constructor(
    private dialogRef: MatDialogRef<LocationPicker>
  ) { }

  ngAfterViewInit(): void {

    this.loadMap();

  }

  loadMap() {

    this.map = L.map('map').setView(
      [this.latitude, this.longitude],
      13
    );

    L.tileLayer(
      'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      {
        maxZoom: 19,
        attribution: '&copy; OpenStreetMap'
      }
    ).addTo(this.map);

    this.marker = L.marker(
      [this.latitude, this.longitude],
      {
        draggable: true
      }
    ).addTo(this.map);

    this.marker.on('dragend', () => {

      const position = this.marker.getLatLng();

      this.latitude = position.lat;

      this.longitude = position.lng;

      this.getAddress();

    });

    this.map.on('click', (event: L.LeafletMouseEvent) => {

      this.latitude = event.latlng.lat;

      this.longitude = event.latlng.lng;

      this.marker.setLatLng(event.latlng);

      this.getAddress();

    });

    this.getAddress();

  }

  useCurrentLocation() {

    if (!navigator.geolocation) {

      alert('Geolocation is not supported.');

      return;

    }

    navigator.geolocation.getCurrentPosition(position => {

      this.latitude = position.coords.latitude;

      this.longitude = position.coords.longitude;

      const latlng = L.latLng(this.latitude, this.longitude);

      this.map.setView(latlng, 16);

      this.marker.setLatLng(latlng);

      this.getAddress();

    });

  }

  async getAddress() {

    try {

      const response = await fetch(

        `https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=${this.latitude}&lon=${this.longitude}`

      );

      const data = await response.json();

      this.address = data.display_name;

    }
    catch {

      this.address = '';

    }

  }

  selectLocation() {

    this.dialogRef.close({

      latitude: this.latitude,

      longitude: this.longitude,

      address: this.address

    });

  }

  cancel() {

    this.dialogRef.close();

  }

}