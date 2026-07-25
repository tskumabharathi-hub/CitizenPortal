using System.ComponentModel.DataAnnotations;

namespace CitizenPortal.DTO.Auth
{
    public class RegisterDto
    {
        [Required]
        public string FirstName {  get; set; } = string.Empty;
        
        [Required] 
        public string SurName { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required] 
        public string Address { get; set; } = string.Empty;
        
        [Required] 
        public string City { get; set; } = string.Empty;
        [Required]  
        public string Pincode { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
