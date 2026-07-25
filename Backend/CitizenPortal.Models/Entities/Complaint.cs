using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.Models.Entities
{
    public class Complaint
    {
        [Key]
        public int ComplaintId {  get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public int DepartmentId {  get; set; }
        [Required]
        public int ComplaintCategoryId {  get; set; }
        

        [Required]
        [StringLength(500)]
        public string Description {  get; set; }=string.Empty;
        public string? ImagePath {  get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
        
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;
        [ForeignKey(nameof(ComplaintCategoryId))]
        public ComplaintCategory ComplaintCategory { get; set; } = null!;
    }
}
