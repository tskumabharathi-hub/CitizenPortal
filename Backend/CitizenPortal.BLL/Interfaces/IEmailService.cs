namespace CitizenPortal.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendOtpAsync(string email, string otp);
    }
}
