using CitizenPortal.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CitizenPortal.DTO.Complaint;
using System.Security.Claims;

namespace CitizenPortal.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComplaint([FromBody] ComplaintDto complaintDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }
            var result = await _complaintService.CreateComplaintAsync(complaintDto,userId);
            if(!result)
            {
                return BadRequest("Unable to create complaint.");
            }
            return Ok(new
            {
                Message ="Complaint submitted successfully."
            });
        }
    }
}
