using CitizenPortal.DTO.Complaint;
using CitizenPortal.Models.Entities;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IComplaintService
    {
        Task<ComplaintResponse> CreateComplaintAsync(ComplaintDto complaintDto,string userId);        
        
        Task<List<ComplaintResponseDto>> GetAllComplaintsAsync();
        
        Task<ComplaintResponseDto?> GetComplaintByIdAsync(Guid complaintId);
        
        Task<List<ComplaintResponseDto>> GetMyComplaintsAsync(string userId);
        
        Task<bool> UpdateComplaintAsync(Guid complaintId,UpdateComplaintDto complaintDto,string userId);
        
        Task<bool> DeleteComplaintAsync(Guid complaintId, string userId);
        
        Task<bool> UpdateComplaintStatusAsync(Guid complaintId,UpdateComplaintStatusDto dto);
    }
}
