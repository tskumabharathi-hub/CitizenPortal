export interface Incident {

  name: string;

  email:string;

  mobile:string;

  complaintId: string;

  department: string;

  complaintCategory: string;

  description: string;

  imagePath?: string;

  latitude: number;

  longitude: number;

  status: string;

  createdDate: Date;

  updatedDate?: Date;

}