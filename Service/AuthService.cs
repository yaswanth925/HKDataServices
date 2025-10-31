using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;
using HKDataServices.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HKDataServices.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IAuthRepository repo, IOptions<JwtSettings> jwtOptions)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _jwtSettings = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(string? email, string? mobile, string password)
{
          var user = await _repo.GetUserByEmailOrMobileAsync(email, mobile);
          if (user == null || !user.IsActive)
            return null;

           bool isPasswordValid = false;

           try
           {
        
             if (user.Password.StartsWith("$2a$") || user.Password.StartsWith("$2b$") || user.Password.StartsWith("$2y$"))
             {
             isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
             }
             else
             {
              if (user.Password == password)
              {
                string newHashed = BCrypt.Net.BCrypt.HashPassword(password);
                await _repo.UpdatePasswordAsync(user.EmailID ?? user.MobileNumber!, newHashed);
                isPasswordValid = true;
              }
             }
           }
             catch
             {
              isPasswordValid = false;
             }

              if (!isPasswordValid)
             return null; 
             var token = GenerateJwtToken(user, out DateTime expiresAt);

            return new AuthResponseDto
            {
               Token = token,
               ExpiresAt = expiresAt,
               User = new UsersResponseDto
               {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailID = user.EmailID,
                MobileNumber = user.MobileNumber ?? string.Empty
               }
            };
        }

        private string GenerateJwtToken(Users user, out DateTime expiresAt)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            expiresAt = now.AddMinutes(_jwtSettings.ExpireMinutes);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailID ?? string.Empty),
                new Claim("mobile", user.MobileNumber ?? string.Empty),
                new Claim("name", $"{user.FirstName} {user.LastName}".Trim()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _repo.GetUserByEmailOrMobileAsync(dto.UserName, dto.UserName);
            if (user == null)
                return (false, "User not found.");

            if (!user.IsActive)
                return (false, "User account is inactive.");
            
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password);
            if (!isPasswordValid)
                return (false, "Current password is incorrect.");

            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            var updated = await _repo.UpdatePasswordAsync(dto.UserName, newHashedPassword);

            if (!updated)
                return (false, "Failed to update password.");

            return (true, "Password changed successfully.");
        }

        public async Task<(bool Success, string Message)> GenerateOtpAsync(string username)
        {
            var user = await _repo.GetUserByEmailOrMobileAsync(username, username);
            if (user == null)
                return (false, "User not found.");

            var otp = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.OtpExpireMinutes);

            await _repo.SaveOtpAsync(username, otp, expiry);

            return (true, $"OTP generated successfully. (Dev: {otp})");
        }

        public async Task<(bool Success, string Message)> VerifyOtpAndChangePasswordAsync(VerifyOtpDto dto)
        {
            var user = await _repo.GetUserByEmailOrMobileAsync(dto.UserName, dto.UserName);
            if (user == null)
                return (false, "User not found.");

            var otpResult = await _repo.VerifyOtpAsync(dto.UserName, dto.OtpCode);
            if (!otpResult.Success)
                return (false, otpResult.Message);

            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            var updated = await _repo.UpdatePasswordAsync(dto.UserName, newHashedPassword);
            if (!updated)
                return (false, "Failed to update password.");

            await _repo.ClearOtpAsync(dto.UserName);

            return (true, "Password updated successfully.");
        }
    }
}