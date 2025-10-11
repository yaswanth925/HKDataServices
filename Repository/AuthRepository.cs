using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthRepository(ApplicationDbContext db) => _db = db;

        public Task<Users> GetByEmailOrMobileAsync(string? email, string? mobile)
        {
            return GetByEmailOrMobileInternalAsync(email, mobile);
        }

        public async Task<Users?> GetUserByEmailOrMobileAsync(string? email, string? mobile)
        {
            return await GetByEmailOrMobileInternalAsync(email, mobile, allowNull: true);
        }

        private async Task<Users?> GetByEmailOrMobileInternalAsync(string? email, string? mobile, bool allowNull = false)
        {
            email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            mobile = string.IsNullOrWhiteSpace(mobile) ? null : mobile.Trim();

            if (mobile == null && email != null && LooksLikeMobile(email))
            {
                mobile = email;
                email = null;
            }

            var user = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    (email != null && u.EmailID == email) ||
                    (mobile != null && u.MobileNumber == mobile));

            if (user == null && !allowNull)
            {
                throw new InvalidOperationException("User not found by email or mobile.");
            }

            return user;
        }

        private static bool LooksLikeMobile(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var digits = value.Where(char.IsDigit).Count();
            return digits == value.Length && digits >= 6 && digits <= 15;
        }

        public Task IncrementFailedAttemptsAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task LockAccountAsync(Guid userId, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task ResetFailedAttemptsAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task RresetFailedAttemptAsync(Guid iD)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePasswordAsync(Guid iD)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ID == userId);
            if (user == null)
                return false;

            user.Password = newPassword;
            //user.ModifiedDate = DateTime.UtcNow;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task SaveOtpAsync(Guid userId, string otpCode, DateTime expiry)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ID == userId);
            if (user != null)
            {
                user.OtpCode = otpCode;
                user.OtpExpiryTime = expiry;
                //user.ModifiedDate = DateTime.UtcNow;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> VerifyOtpAsync(string username, string otpCode)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u =>
                    (u.EmailID == username || u.MobileNumber == username) &&
                    u.OtpCode == otpCode);

            if (user == null) return false;
            if (user.OtpExpiryTime == null || user.OtpExpiryTime < DateTime.UtcNow) return false;

            return true;
        }

        public async Task ClearOtpAsync(Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ID == userId);
            if (user != null)
            {
                user.OtpCode = null;
                user.OtpExpiryTime = null;
                //user.ModifiedDate = DateTime.UtcNow;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}