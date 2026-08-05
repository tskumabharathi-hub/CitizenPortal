using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DAL.Data;
using CitizenPortal.DTO.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.BLL.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        public DepartmentService (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmrntName = d.DepartmentName,
                    Icon = d.Icon
                })
                .ToListAsync();
        }
    }
}
