using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> AuthenticateAsync(string? email, string? mobile, string password);
        Task AuthenticateAsync(string username, string password);
        bool ValidateToken(string token);
        Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDto dto);
        Task<(bool Success, string Message)> GenerateOtpAsync(string username);
        Task<(bool Success, string Message)> VerifyOtpAndChangePasswordAsync(VerifyOtpDto dto);
    }
}