
using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HKDataServices.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthRepository(ApplicationDbContext db) => _db = db;

        public Task<Users> GetByEmailOrMobileAsync(string? email, string? mobile)
        {
            throw new NotImplementedException();
        }

        public async Task<Users?> GetUserByEmailOrMobileAsync(string? email, string? mobile)
        {
            email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            mobile = string.IsNullOrWhiteSpace(mobile) ? null : mobile.Trim();

            return await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    (email != null && u.EmailID == email) ||
                    (mobile != null && u.MobileNumber == mobile));
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

    }
}
