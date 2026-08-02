import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-success-dialog',
  standalone: true,
  imports: [MatButtonModule,DatePipe],
  templateUrl: './success-dialog.html',
  styleUrls: ['./success-dialog.css']
})
export class SuccessDialog {

  constructor(
    public dialogRef: MatDialogRef<SuccessDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ok() {
    this.dialogRef.close(true);
  }

}