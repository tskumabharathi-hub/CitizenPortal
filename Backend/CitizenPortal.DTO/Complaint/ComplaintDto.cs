using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CitizenPortal.DTO.Complaint
{
    public class ComplaintDto
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ComplaintCategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public IFormFile? Photo { get; set; }

        public string? ImagePath { get; set; }
    }
}