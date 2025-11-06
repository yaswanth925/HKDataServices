using HKDataServices.Controllers.API;
using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class PreSalesTargetListRepository : IPreSalesTargetListRepository
    {
        private readonly ApplicationDbContext _context;

        public PreSalesTargetListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PreSalesTargetList>> GetAllAsync(CancellationToken ct)
        {
            return await _context.PreSalesTargetList
                                 .AsNoTracking()
                                 .Include(x => x.PreSalesTarget) 
                                 .OrderBy(x => x.EmployeeName)
                                 .ToListAsync(ct);
        }
    }
}
