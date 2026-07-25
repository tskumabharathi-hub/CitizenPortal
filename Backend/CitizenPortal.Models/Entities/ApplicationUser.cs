using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public string Gender {  get; set; } = string.Empty;

        public string Address {  get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public string Pincode {  get; set; } = string.Empty;

        public ICollection<Complaint> Complaints { get; set; }
           = new List<Complaint>();
    }
}
