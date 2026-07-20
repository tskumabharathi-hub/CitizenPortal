import { Component,EventEmitter, Output } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})

export class Header {

  userName:string = 'CITIZEN'
  @Output() profileClick = new EventEmitter<void>();

  openProfile(){
    this.profileClick.emit();
  }

}