using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
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

        public async Task<IEnumerable<PreSalesTarget>> GetByEmployeeNameAsync(string employeeName)
        {
            return await _context.PreSalesTargets
             .Where(x => !string.IsNullOrEmpty(x.EmployeeName) && x.EmployeeName.ToLower() == employeeName.ToLower())
             .ToListAsync();
        }

        public async Task AddAsync(PreSalesTarget target)
        {
            await _context.PreSalesTargets.AddAsync(target);
            await _context.SaveChangesAsync();
        }
    }
}
