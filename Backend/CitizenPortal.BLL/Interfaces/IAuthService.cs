using CitizenPortal.DTO.Auth;
using CitizenPortal.Models.Entities;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<ApiResponse> VerifyOtpAsync(VerifyOtpDto dto);
    }
}
