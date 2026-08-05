using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CitizenPortal.DTO.Complaint
{
    public class UpdateComplaintStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
