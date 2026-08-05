using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CitizenPortal.DTO.ComplaintCategory
{
    public class ComplaintCategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public int DepartmentId { get; set; }
    }
}