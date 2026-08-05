using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DTO.Complaint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CitizenPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailService emailService;

        public ComplaintController(
            IComplaintService complaintService,
            IWebHostEnvironment environment,
            IEmailService emailService)
        {
            _complaintService = complaintService;
            _environment = environment;
            this.emailService = emailService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComplaint([FromForm] ComplaintDto complaintDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            // Save uploaded image
            if (complaintDto.Photo != null)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "ComplaintImages");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(complaintDto.Photo.FileName);

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await complaintDto.Photo.CopyToAsync(stream);
                }

                complaintDto.ImagePath = "/ComplaintImages/" + fileName;
            }

            string? email = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
                .Value;

            var response = await _complaintService.CreateComplaintAsync(
                complaintDto,
                userId);

            if (response == null)
            {
                return BadRequest("Unable to create complaint.");
            }

            var isMessageSent = emailService.SendIncidentNotification(response.ComplaintId.ToString(), response.CreatedDate.ToString(), email);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllComplaints()
        {
            var complaints = await _complaintService.GetAllComplaintsAsync();

            return Ok(complaints);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComplaintById(Guid id)
        {
            var complaint = await _complaintService.GetComplaintByIdAsync(id);

            if (complaint == null)
            {
                return NotFound(new
                {
                    Message = "Complaint not found."
                });
            }

            return Ok(complaint);
        }


        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyComplaints()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            var complaints = await _complaintService.GetMyComplaintsAsync(userId);

            return Ok(complaints);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComplaint(Guid id,[FromForm] UpdateComplaintDto complaintDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            // Save uploaded image (if a new one is provided)
            if (complaintDto.Photo != null)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "ComplaintImages");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(complaintDto.Photo.FileName);

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await complaintDto.Photo.CopyToAsync(stream);
                }

                complaintDto.ImagePath = "/ComplaintImages/" + fileName;
            }

            var result = await _complaintService.UpdateComplaintAsync(
                id,
                complaintDto,
                userId);

            if (!result)
            {
                return NotFound(new
                {
                    Message = "Complaint not found or you are not authorized to update it."
                });
            }

            return Ok(new
            {
                Message = "Complaint updated successfully."
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            var result = await _complaintService.DeleteComplaintAsync(id, userId);

            if (!result)
            {
                return NotFound(new
                {
                    Message = "Complaint not found or you are not authorized to delete it."
                });
            }

            return Ok(new
            {
                Message = "Complaint deleted successfully."
            });
        }
        
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateComplaintStatus(Guid id,[FromBody] UpdateComplaintStatusDto dto)
        {
            var result = await _complaintService
                .UpdateComplaintStatusAsync(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    Message = "Complaint not found."
                });
            }

            return Ok(new
            {
                Message = "Complaint status updated successfully."
            });
        }
    }
}
