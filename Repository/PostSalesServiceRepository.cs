using HKDataServices.Controllers.API;
using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class PostSalesServiceRepository : IPostSalesServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public PostSalesServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostSalesService>> GetAllAsync(CancellationToken ct)
        {
            return await _context.PostSalesService
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<PostSalesService?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.PostSalesService
                .AsNoTracking()
                .FirstOrDefaultAsync(psa => psa.PostSalesServiceID == id, ct);
        }

        public async Task CreateAsync(PostSalesService entity, CancellationToken ct)
        {
            await _context.PostSalesService.AddAsync(entity, ct);
        }

        public void Update(PostSalesService entity)
        {
            _context.PostSalesService.Update(entity);
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
