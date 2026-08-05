using CitizenPortal.DTO.Auth;
using CitizenPortal.Models.Entities;
using System.Security.Claims;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<ApiResponse> VerifyOtpAsync(VerifyOtpDto dto);

        Task<ProfileResponseDto?> GetProfileAsync(ClaimsPrincipal user);

        Task<ApiResponse> UpdateProfileAsync(string userId,UpdateProfileDto dto);
    }
}
