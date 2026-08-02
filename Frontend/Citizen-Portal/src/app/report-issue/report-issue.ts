import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LocationPicker } from '../location-picker/location-picker';
import { MatDialog } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { ComplaintResponse } from '../models/complaint-response';
import { Complaint } from '../models/Complaint';
import { SuccessDialog } from '../success-dialog/success-dialog';

@Component({
  selector: 'app-report-issue',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './report-issue.html',
  styleUrls: ['./report-issue.css']
})
export class ReportIssue{

  reportForm: FormGroup;
  base64Image: string = '';
  apiUrl:string = 'http://localhost:5298/api/Complaint';
  selectedFile: File | null = null;
  preview: string | ArrayBuffer | null = null;
  incidentId:string = '';
  status:string ='';
  showSuccess:boolean = false;
  departmentMap = new Map<string, number>();
  issueMap = new Map<string, number>();

  departments = [
    {
      name: 'Public Work Department',
      issues: ['Pothole Repair','Broken Street Light']
    },
    {
      name: 'Department of Buildings',
      issues: ['Code Violation','Public Housing']
    },
    {
      name: 'Water Department',
      issues: ['Water Leak','Unsafe Drinking Water']
    },
    {
      name: 'Department of Sanitation',
      issues: ['Waste Collection','Recycling']
    }
  ];

  issues:string[]=[];

  constructor(private fb:FormBuilder,private dialog: MatDialog,private http: HttpClient,){

    this.reportForm=this.fb.group({

      department:['',Validators.required],
      issue:['',Validators.required],
      description:['',Validators.required],
      address:['',Validators.required],
      latitude:[''],
      longitude:[''],
      photo:[null]

    });

      this.departmentMap.set('Public Work Department', 1);
      this.departmentMap.set('Department of Buildings', 2);
      this.departmentMap.set('Water Department', 3);
      this.departmentMap.set('Department of Sanitation', 4);

      this.issueMap.set('Pothole Repair', 1);
      this.issueMap.set('Broken Street Light', 2);
      this.issueMap.set('Code Violation', 3);
      this.issueMap.set('Public Housing', 4);
      this.issueMap.set('Water Leak', 5);
      this.issueMap.set('Unsafe Drinking Water', 6);
      this.issueMap.set('Recycling', 7);
      this.issueMap.set('Garbage Collection', 8);

  }

  departmentChanged()
  {
    const dep=this.reportForm.value.department;
    const dept=this.departments.find(x=>x.name==dep);
    this.issues=dept?dept.issues:[];
    this.reportForm.patchValue({
      issue:''
    });

  }

  upload(event: Event): void {
    const input = event.target as HTMLInputElement;
    
    if (!input.files || input.files.length === 0) 
    {
      return;
    }

    this.selectedFile = input.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.preview = reader.result;
      this.base64Image = (reader.result as string).split(',')[1];
    };
    reader.readAsDataURL(this.selectedFile);
  }

  openLocationPicker()
  {
    const dialogRef = this.dialog.open(LocationPicker,{
        width:'1000px',
        maxWidth:'95vw',
        height:'760px'
    });

    dialogRef.afterClosed().subscribe(result => {
        if (result) {
            this.reportForm.patchValue({
                address: result.address,
                latitude: result.latitude,
                longitude: result.longitude
            });
        }
    });
  }

  submit() {

    if (this.reportForm.invalid) {
        return;
    }

    const formData = new FormData();

    const departmentId = this.departmentMap.get(this.reportForm.value.department);
    const issueId = this.issueMap.get(this.reportForm.value.issue);

    if (departmentId === undefined || issueId === undefined) {
        alert('Please select a valid Department and Issue.');
        return;
    }


    const complaint: Complaint = {
      DepartmentId: departmentId,
      ComplaintCategoryId: issueId,
      Description: this.reportForm.value.description,
      Latitude: this.reportForm.value.latitude,
      Longitude: this.reportForm.value.longitude,
    };

    const data = this.createFormData(complaint);

    this.http.post<ComplaintResponse>(
        this.apiUrl,
        data
    ).subscribe(
      {
        next: (response)=>
        {

          console.log(response);

          const dialogRef = this.dialog.open(SuccessDialog,{
                  width:'460px',
                  disableClose:true,
                  data:{
                    incidentId: response.complaintId,
                    status: response.status,
                    date: response.createdDate
                  }
          });

          dialogRef.afterClosed().subscribe(()=>{
            this.reportForm.reset();
            this.preview = null;
            this.selectedFile = null;
            this.reportForm.patchValue({
                latitude:'',
                longitude:'',
                address:''
            });
          });
        },
        error: ()=>{
            alert("Unable to submit complaint.");
        }
      }
    );
  }

  private createFormData(complaint: Complaint): FormData {

    const formData = new FormData();

    formData.append('DepartmentId', complaint.DepartmentId.toString());

    formData.append('ComplaintCategoryId', complaint.ComplaintCategoryId.toString());

    formData.append('Description', complaint.Description);

    formData.append('Latitude', complaint.Latitude.toString());

    formData.append('Longitude', complaint.Longitude.toString());

    if (this.selectedFile) {

      formData.append(
        'Photo',
        this.selectedFile,
        this.selectedFile.name
      );

    }

    return formData;
  }

}