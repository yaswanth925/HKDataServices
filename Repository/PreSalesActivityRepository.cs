using HKDataServices.Controllers.API;
using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class PreSalesActivityRepository : IPreSalesActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public PreSalesActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PreSalesActivity>> GetAllAsync(CancellationToken ct)
        {
            return await _context.PreSalesActivity
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<PreSalesActivity?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.PreSalesActivity
                .AsNoTracking()
                .FirstOrDefaultAsync(psa => psa.PreSalesActivityID == id, ct);
        }

        public async Task CreateAsync(PreSalesActivity entity, CancellationToken ct)
        {
            await _context.PreSalesActivity.AddAsync(entity, ct);
        }

        public void Update(PreSalesActivity entity)
        {
            _context.PreSalesActivity.Update(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
