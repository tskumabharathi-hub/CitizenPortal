using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.Models.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
        public string? Icon {  get; set; }

        public ICollection<ComplaintCategory> ComplaintCategories { get; set; }
        = new List<ComplaintCategory>();
        public ICollection<Complaint> Complaints { get; set; }
        = new List<Complaint>();
    }
}
