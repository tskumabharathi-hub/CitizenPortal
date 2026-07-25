using CitizenPortal.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitizenPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintCategoryController : ControllerBase
    {
        private readonly IComplaintCategoryService _complaintCategoryService;

        public ComplaintCategoryController(IComplaintCategoryService complaintCategoryService)
        {
            _complaintCategoryService = complaintCategoryService;
        }

        [HttpGet("ByDepartment/{departmentId}")]
        public async Task<IActionResult> GetCategoriesByDepartment(int departmentId)
        {
            var categories = await _complaintCategoryService
                .GetCategoriesByDepartmentAsync(departmentId);

            return Ok(categories);
        }
    }
}