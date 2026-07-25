using CitizenPortal.DTO.Complaint;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IComplaintService
    {
        Task<bool> CreateComplaintAsync(
            ComplaintDto complaintDto,
            string userId);
    }
}
