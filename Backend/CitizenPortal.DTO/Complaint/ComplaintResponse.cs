namespace CitizenPortal.DTO.Complaint
{
    public class ComplaintResponse
    {
        public Guid ComplaintId { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
