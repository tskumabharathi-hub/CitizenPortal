using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.Models.Entities
{
    public class ComplaintCategory
    {
        [Key]
        public int CategoryId {  get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Icon {  get; set; }
        public int DepartmentId {  get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public  Department Department {  get; set; }= null!;
        public ICollection<Complaint> Complaints { get; set; }
        = new List<Complaint>();
    }
}
