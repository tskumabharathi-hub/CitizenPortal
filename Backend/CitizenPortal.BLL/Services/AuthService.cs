using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DTO.Auth;
using CitizenPortal.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CitizenPortal.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }
            bool isPasswordValid =
                await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry =
                DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: credentials);

            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiry
            };
        }
        
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already exists."
                };
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = string.Empty,
                Surname = string.Empty,
                Gender = string.Empty,
                Address = string.Empty,
                City = string.Empty,
                Pincode = string.Empty,
                EmailConfirmed = false,
                EmailOtp = registerDto.OTP,
                OtpExpiry = DateTime.UtcNow.AddMinutes(10)

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(",", result.Errors.Select(e => e.Description))
                };
            }
            await _userManager.AddToRoleAsync(user, "User");
            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Registration successful."
            };
        }

        public async Task<ApiResponse> VerifyOtpAsync(VerifyOtpDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            if (user.EmailConfirmed)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Email already verified."
                };
            }

            if (user.OtpExpiry == null || user.OtpExpiry < DateTime.UtcNow)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "OTP has expired."
                };
            }

            if (user.EmailOtp != dto.Otp)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Invalid OTP."
                };
            }

            user.EmailConfirmed = true;
            user.EmailOtp = null;
            user.OtpExpiry = null;

            await _userManager.UpdateAsync(user);

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Email verified successfully."
            };
        }

        public async Task<ProfileResponseDto?> GetProfileAsync(ClaimsPrincipal user)
        {
            // Read UserId from JWT
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            // Fetch user from Identity
            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                return null;
            }

            // Map entity to DTO
            return new ProfileResponseDto
            {
                FirstName = applicationUser.FirstName,
                Surname = applicationUser.Surname,
                Email = applicationUser.Email ?? string.Empty,
                Gender = applicationUser.Gender,
                Address = applicationUser.Address,
                City = applicationUser.City,
                Pincode = applicationUser.Pincode,
                PhoneNumber = applicationUser.PhoneNumber
            };
        }

        public async Task<ApiResponse> UpdateProfileAsync(string userId,UpdateProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            user.FirstName = dto.FirstName;
            user.Surname = dto.Surname;
            user.Gender = dto.Gender;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            user.City = dto.City;
            user.Pincode = dto.Pincode;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.First().Description
                };
            }

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Profile updated successfully."
            };
        }

    }
}
