using CitizenPortal.DTO.Department;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartmentsAsync();
    }
}
