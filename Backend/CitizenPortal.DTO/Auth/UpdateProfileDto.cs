namespace CitizenPortal.DTO.Auth
{
    public class UpdateProfileDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;
    }
}
