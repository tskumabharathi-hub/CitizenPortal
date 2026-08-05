using Microsoft.AspNetCore.Identity;

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

        public string? EmailOtp { get; set; }

        public DateTime? OtpExpiry { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
           = new List<Complaint>();
    }
}
