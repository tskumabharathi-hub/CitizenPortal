using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CitizenPortal.BLL.Interfaces
{
    public interface IComplaintCategoryService
    {
        Task<List<DTO.ComplaintCategory.ComplaintCategoryDto>> GetCategoriesByDepartmentAsync(int departmentId);
    }
}
