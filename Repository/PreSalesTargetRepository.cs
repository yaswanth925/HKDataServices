using HKDataServices.Controllers.API;
using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;


namespace HKDataServices.Repository
{
    public class PreSalesTargetRepository : IPreSalesTargetRepository
    {
        private readonly ApplicationDbContext _context;

        public PreSalesTargetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PreSalesTarget>> GetAllAsync(CancellationToken ct)
        {
            return await _context.PreSalesTarget
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<PreSalesTarget> GetByEmployeeNameAsync(string employeeName, CancellationToken ct)
        {
            return await _context.PreSalesTarget
                .AsNoTracking()
                .FirstOrDefaultAsync(psa => psa.EmployeeName == employeeName,ct);
        }

        public async Task AddAsync(PreSalesTarget entity)
        {
            entity.TargetID = Guid.NewGuid();
            entity.Created = DateTime.UtcNow;
            _context.Set<PreSalesTarget>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PreSalesTarget entity)
        {
            entity.Modified = DateTime.UtcNow;
            _context.Set<PreSalesTarget>().Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}