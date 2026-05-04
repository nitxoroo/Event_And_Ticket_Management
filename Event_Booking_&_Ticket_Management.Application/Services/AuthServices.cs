using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using Event_Booking___Ticket_Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthServices(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterResponseDto> Register(RegisterDto dto)
        {
            User newUser = new User
            {
                Name = dto.Name,
                UserName = dto.Name,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            await _userManager.AddToRoleAsync(newUser, "User");
            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return new RegisterResponseDto
            {
                UserName = newUser.Name,
                Email = newUser.Email
            };
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new Exception("Invalid Email or Password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            string JwtToken = GenerateJwtToken(user,roles);
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken=refreshToken;
            user.RefreshTokenExpiryTime= DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["RefreshToken:Expiration_Minutes"])
            );

            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                JwtToken = JwtToken,
                JwtTokenExpirationTime = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])),
                RefreshToken = refreshToken,
                RefreshTokenExpirationTime = user.RefreshTokenExpiryTime.ToString()
            };

        }
        public async Task<AuthResponseDto> RefreshToken(string refreshToken)
        {
            var user = _userManager.Users
                .FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
                throw new Exception("No user found");

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            var newJwtToken = GenerateJwtToken(user, await _userManager.GetRolesAsync(user));
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["RefreshToken:Expiration_Minutes"])
            );
            await _userManager.UpdateAsync(user);
            return new AuthResponseDto
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                JwtToken = newJwtToken,
                JwtTokenExpirationTime = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])),

                RefreshToken = newRefreshToken,
                RefreshTokenExpirationTime = user.RefreshTokenExpiryTime.ToString()
            };

        }

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };

            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            DateTime expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"])
                );

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
