using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IAuthRepository
    {
        Task<Users?> GetUserByEmailOrMobileAsync(string? email, string? mobile);
        Task<Users> GetByEmailOrMobileAsync(string? email, string? mobile);
        Task SaveOtpAsync(string username, string otpCode, DateTime expiry);
        Task<(bool Success, string Message)> VerifyOtpAsync(string username, string otpCode);
        Task ClearOtpAsync(string username);
        Task<bool> UpdatePasswordAsync(string username, string newPassword);

    }
}
