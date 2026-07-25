using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace CitizenPortal.DTO.Complaint
{
    public class ComplaintDto
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ComplaintCategoryId { get; set; }

        //[Required]
        //public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public string? ImagePath { get; set; }
    }
}