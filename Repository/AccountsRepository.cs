using HKDataServices.Controllers.API;
using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accounts>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Accounts
                .AsNoTracking()
                .OrderBy(a => a.DealerName)
                .ToListAsync(ct);
        }

        public async Task<Accounts?> GetByDealerCodeAsync(int dealerCode)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.DealerCode == dealerCode);
        }

        public async Task<Accounts?> GetByDealerNameAsync(string dealerName)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.DealerName == dealerName);
        }

        public async Task CreateAsync(Accounts entity, CancellationToken ct)
        {
            await _context.Accounts.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Accounts entity, CancellationToken ct)
        {
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
