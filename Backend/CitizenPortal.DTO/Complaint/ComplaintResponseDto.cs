namespace CitizenPortal.DTO.Complaint
{
    public class ComplaintResponseDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public Guid ComplaintId { get; set; }

        public string Department { get; set; } = string.Empty;

        public string ComplaintCategory { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
