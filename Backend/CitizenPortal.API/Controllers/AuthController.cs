using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitizenPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;
        private readonly IEmailService _emailService;
        private string otp = string.Empty;
        public AuthController(IAuthService authservice,IEmailService emailService)
        {
            _authservice = authservice;
            _emailService = emailService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authservice.LoginAsync(loginDto);
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
            var res = await _authservice.RegisterAsync(registerDto);

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
            var result = await _authservice.VerifyOtpAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
