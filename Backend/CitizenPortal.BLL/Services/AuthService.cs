using CitizenPortal.BLL.Interfaces;
using CitizenPortal.DTO.Auth;
using CitizenPortal.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                FirstName = registerDto.FirstName,
                Surname = registerDto.SurName,
                Gender = registerDto.Gender,
                Address = registerDto.Address,
                City = registerDto.City,
                Pincode = registerDto.Pincode,
                EmailConfirmed = true
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
    }
}
