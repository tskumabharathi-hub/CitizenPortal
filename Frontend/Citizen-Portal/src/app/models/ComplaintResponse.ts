export interface ComplaintResp {

    complaintId: string;
    createdDate: string;
    updatedDate: string | null;
    status: string;
    description: string;
    departmentName: string;
    complaintCategoryName: string;
    imagePath: string | null;

}