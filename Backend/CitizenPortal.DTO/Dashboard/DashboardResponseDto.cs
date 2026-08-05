namespace CitizenPortal.DTO.Dashboard
{
    public class DashboardResponseDto
    {
        public int TotalComplaints { get; set; }

        public int PendingComplaints { get; set; }

        public int InProgressComplaints { get; set; }

        public int ResolvedComplaints { get; set; }

        public int TodayComplaints { get; set; }

        public List<RecentComplaintDto> RecentComplaints { get; set; } = new();
    }

    public class RecentComplaintDto
    {
        public Guid ComplaintId { get; set; }

        public string Department { get; set; } = string.Empty;

        public string ComplaintCategory { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
