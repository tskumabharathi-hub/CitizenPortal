using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DAL.Data;
using CitizenPortal.DTO.Complaint;
using CitizenPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.BLL.Services
{
    public class ComplaintService:IComplaintService
    {
        private readonly ApplicationDbContext _context;
        public ComplaintService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateComplaintAsync(
            ComplaintDto complaintDto,
            string userId)
        {
            //throw new NotImplementedException();
            var complaint = new Complaint
            {
                UserId = userId,
                DepartmentId = complaintDto.DepartmentId,
                ComplaintCategoryId = complaintDto.ComplaintCategoryId,
                Description = complaintDto.Description,
                Latitude = complaintDto.Latitude,
                Longitude = complaintDto.Longitude,
                ImagePath = complaintDto.ImagePath,
                Status = "Pending",
                CreatedDate = DateTime.Now,
            };
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
