using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IAuthRepository
    {
        Task<Users?> GetUserByEmailOrMobileAsync(string? email, string? mobile);
        Task<Users> GetByEmailOrMobileAsync(string? email, string? mobile);
        Task UpdatePasswordAsync(Guid iD);
        Task SaveOtpAsync(Guid userId, string otpCode, DateTime expiry);
        Task<bool> VerifyOtpAsync(string username, string otpCode);
        Task ClearOtpAsync(Guid userId);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);

    }
}
