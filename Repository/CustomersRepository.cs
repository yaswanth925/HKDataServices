using HKDataServices.Model;
using HKDataServices.Controllers.API;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
            => await _context.Customers.ToListAsync();

        public async Task<Customers?> GetByIdAsync(Guid id)
            => await _context.Customers.FindAsync(id);

        public async Task CreateAsync(Customers entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customers entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
