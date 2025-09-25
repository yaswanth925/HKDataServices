using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HKDataServices.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Users> InsertAsync(Users entity, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
        public async Task<Users?> GetByMobileNumberAsync(string mobileNumber, CancellationToken ct = default)
        {
            return await _db.Users
                .AsNoTracking()
                .Where(x => x.MobileNumber == mobileNumber)
                .OrderByDescending(x => x.Created)
                .ThenByDescending(x => x.ID)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<Users?> GetByEmailIDAsync(string emailID, CancellationToken ct = default)
        {
            return await _db.Users
                .AsNoTracking()
                .Where(x => x.EmailID == emailID)
                .OrderByDescending(x => x.Created)
                .ThenByDescending(x => x.ID)
                .FirstOrDefaultAsync(ct);
        }
        public async Task<Users?> GetByEmailOrMobileAsync(string? email, string? mobile)
        {
            return await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    (!string.IsNullOrEmpty(email) && u.EmailID == email) ||
                    (!string.IsNullOrEmpty(mobile) && u.MobileNumber == mobile));
        }


    }
}
    
