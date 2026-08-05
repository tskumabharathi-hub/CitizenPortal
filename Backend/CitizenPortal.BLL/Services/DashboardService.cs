using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DAL.Data;
using CitizenPortal.DTO.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace CitizenPortal.BLL.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly ApplicationDbContext _context;
        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponseDto> GetDashboardAsync()
        {
            var today = DateTime.Today;

            DashboardResponseDto response = new DashboardResponseDto();

            response.TotalComplaints = await _context.Complaints.CountAsync();

            response.PendingComplaints = await _context.Complaints
                .CountAsync(x => x.Status == "Pending");

            response.InProgressComplaints = await _context.Complaints
                .CountAsync(x => x.Status == "In Progress");

            response.ResolvedComplaints = await _context.Complaints
                .CountAsync(x => x.Status == "Resolved");

            response.TodayComplaints = await _context.Complaints
                .CountAsync(x => x.CreatedDate.Date == today);

            response.RecentComplaints = await _context.Complaints
                .OrderByDescending(x => x.CreatedDate)
                .Take(5)
                .Select(x => new RecentComplaintDto
                {
                    ComplaintId = x.ComplaintId,
                    Department = x.Department.DepartmentName,
                    ComplaintCategory = x.ComplaintCategory.CategoryName,
                    Status = x.Status,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync();

            return response;
        }
    }
}
