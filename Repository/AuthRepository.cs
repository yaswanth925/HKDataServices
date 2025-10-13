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

        public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => (u.EmailID == username || u.MobileNumber == username));
            if (user == null)
                return false;

            user.Password = newPassword;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task SaveOtpAsync(string username, string otpCode, DateTime expiry)
        {            
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            var user = await _db.Users
                .FirstOrDefaultAsync(u =>
                    u.EmailID.ToLower() == username.ToLower() ||
                    u.MobileNumber == username);

            if (user == null)
                return;

            if (!user.IsActive)
                return; 

            user.OtpCode = otpCode;
            user.OtpExpiryTime = expiry;
            user.Modified = DateTime.UtcNow;

            _db.Entry(user).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        public async Task<(bool Success, string Message)> VerifyOtpAsync(string username, string otpCode)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(otpCode))
                return (false, "Username or OTP cannot be empty.");

            var user = await _db.Users.FirstOrDefaultAsync(u =>(u.EmailID == username || u.MobileNumber == username));

            if (user == null)
                return (false, "User not found.");

            if (string.IsNullOrEmpty(user.OtpCode) || user.OtpExpiryTime == null)
                return (false, "No OTP found. Please generate a new one.");

            if (user.OtpCode != otpCode)
                return (false, "Incorrect OTP. Please try again.");

            if (user.OtpExpiryTime < DateTime.UtcNow)
                return (false, "OTP expired. Please request a new one.");

            return (true, "OTP verified successfully.");
        }
        public async Task ClearOtpAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => (u.EmailID == username || u.MobileNumber == username));

            if (user != null)
            {
                user.OtpCode = null;
                user.OtpExpiryTime = null;
                user.Modified = DateTime.UtcNow;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}