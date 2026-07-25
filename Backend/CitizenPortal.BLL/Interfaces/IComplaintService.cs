using CitizenPortal.DTO.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IComplaintService
    {
        Task<bool> CreateComplaintAsync(
            ComplaintDto complaintDto,
            string userId);
    }
}
