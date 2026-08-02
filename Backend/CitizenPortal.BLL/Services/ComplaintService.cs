using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DAL.Data;
using CitizenPortal.DTO.Complaint;
using CitizenPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizenPortal.BLL.Services
{
    public class ComplaintService:IComplaintService
    {
        private readonly ApplicationDbContext _context;
        public ComplaintService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ComplaintResponse> CreateComplaintAsync(ComplaintDto complaintDto,string userId)
        {
            ComplaintResponse response = new ComplaintResponse();
            try
            {
                Guid complaintId = Guid.NewGuid();
                var complaint = new Complaint
                {
                    ComplaintId = complaintId,
                    UserId = userId,
                    DepartmentId = complaintDto.DepartmentId,
                    ComplaintCategoryId = complaintDto.ComplaintCategoryId,
                    Description = complaintDto.Description,
                    Latitude = complaintDto.Latitude,
                    Longitude = complaintDto.Longitude,
                    ImagePath = complaintDto.ImagePath,
                    Status = "Pending",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _context.Complaints.Add(complaint);
                await _context.SaveChangesAsync();
                response.ComplaintId = complaintId;
                response.Status = complaint.Status;
                response.CreatedDate = complaint.CreatedDate;
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //get allcomplaints
        public async Task<List<ComplaintResponseDto>> GetAllComplaintsAsync()
        {
            return await _context.Complaints
                .Include(c => c.Department)
                .Include(c => c.ComplaintCategory)
                .Select(c => new ComplaintResponseDto
                {
                    ComplaintId = c.ComplaintId,
                    Department = c.Department.DepartmentName,
                    ComplaintCategory = c.ComplaintCategory.CategoryName,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();
        }
        //get complaints by ID
        public async Task<ComplaintResponseDto?> GetComplaintByIdAsync(Guid complaintId)
        {
            return await _context.Complaints
                .Include(c => c.Department)
                .Include(c => c.ComplaintCategory)
                .Where(c => c.ComplaintId == complaintId)
                .Select(c => new ComplaintResponseDto
                {
                    Name = c.User.FirstName,
                    Email = c.User.Email,
                    Mobile = c.User.PhoneNumber,
                    ComplaintId = c.ComplaintId,
                    Department = c.Department.DepartmentName,
                    ComplaintCategory = c.ComplaintCategory.CategoryName,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate
                })
                .FirstOrDefaultAsync();
        }
        public async Task<List<ComplaintResponseDto>> GetMyComplaintsAsync(string userId)
        {
            return await _context.Complaints
                .Include(c => c.Department)
                .Include(c => c.ComplaintCategory)
                .Where(c => c.UserId == userId)
                .Select(c => new ComplaintResponseDto
                {
                    ComplaintId = c.ComplaintId,
                    Department = c.Department.DepartmentName,
                    ComplaintCategory = c.ComplaintCategory.CategoryName,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate
                })
                .ToListAsync();
        }
        public async Task<bool> UpdateComplaintAsync(Guid complaintId,UpdateComplaintDto complaintDto,string userId)
        {
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c =>
                    c.ComplaintId == complaintId &&
                    c.UserId == userId);

            if (complaint == null)
            {
                return false;
            }

            complaint.DepartmentId = complaintDto.DepartmentId;
            complaint.ComplaintCategoryId = complaintDto.ComplaintCategoryId;
            complaint.Description = complaintDto.Description;
            complaint.Latitude = complaintDto.Latitude;
            complaint.Longitude = complaintDto.Longitude;
            if (!string.IsNullOrEmpty(complaintDto.ImagePath))
            {
                complaint.ImagePath = complaintDto.ImagePath;
            }
            complaint.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteComplaintAsync(Guid complaintId, string userId)
        {
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c =>
                    c.ComplaintId == complaintId &&
                    c.UserId == userId);

            if (complaint == null)
            {
                return false;
            }

            _context.Complaints.Remove(complaint);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateComplaintStatusAsync(Guid complaintId,UpdateComplaintStatusDto dto)
        {
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.ComplaintId == complaintId);

            if (complaint == null)
            {
                return false;
            }

            complaint.Status = dto.Status;
            complaint.UpdatedDate = DateTime.Now;
            complaint.Remarks = dto.Remarks;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
