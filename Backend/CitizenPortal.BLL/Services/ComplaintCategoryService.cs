using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DAL.Data;
using CitizenPortal.DTO.ComplaintCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.BLL.Services
{
    public class ComplaintCategoryService :IComplaintCategoryService
    {
        private readonly ApplicationDbContext _context;
        public ComplaintCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }   
        public async Task<List<ComplaintCategoryDto>> GetCategoriesByDepartmentAsync(int departmentId)
        {
            return await _context.ComplaintCategories
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new ComplaintCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Icon = c.Icon,
                    DepartmentId = c.DepartmentId
                })
                .ToListAsync();
        }
    }
}
