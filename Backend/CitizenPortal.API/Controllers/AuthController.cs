using CitizenPortal.BLL.Interfaces;
using CitizenPortal.BLL.Services;
using CitizenPortal.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CitizenPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private string otp = string.Empty;
        public AuthController(IAuthService authservice,IEmailService emailService)
        {
            _authService = authservice;
            _emailService = emailService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var random = new Random();
            otp = random.Next(10000, 99999).ToString();
            registerDto.OTP = otp;
            var res = await _authService.RegisterAsync(registerDto);

            if (!res.IsSuccess)
            {
                return BadRequest(res);
            }

            bool result = await _emailService.SendOtpAsync(registerDto.Email,otp);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
        {
            var result = await _authService.VerifyOtpAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _authService.GetProfileAsync(User);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _authService.UpdateProfileAsync(userId, dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
