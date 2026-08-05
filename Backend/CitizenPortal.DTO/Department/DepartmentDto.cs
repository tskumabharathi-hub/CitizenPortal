using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.DTO.Department
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string DepartmrntName { get; set; }= string.Empty;
        public string? Icon {  get; set; }
    }
}
