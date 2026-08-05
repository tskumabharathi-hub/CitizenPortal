namespace CitizenPortal.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendOtpAsync(string email, string otp);

        Task<bool> SendIncidentNotification(string complaintId, string otp,string email);
    }
}
