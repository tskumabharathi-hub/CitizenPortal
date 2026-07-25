namespace CitizenPortal.BLL.Interfaces
{
    public interface IComplaintCategoryService
    {
        Task<List<DTO.ComplaintCategory.ComplaintCategoryDto>> GetCategoriesByDepartmentAsync(int departmentId);
    }
}
