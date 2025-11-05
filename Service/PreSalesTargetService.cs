using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;


namespace HKDataServices.Service
{
    public class PreSalesTargetService : IPreSalesTargetService
    {
        private readonly IPreSalesTargetRepository _repository;
        private readonly ApplicationDbContext? _context;
        private ApplicationDbContext? context;

        public PreSalesTargetService(IPreSalesTargetRepository repository)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<PreSalesTarget>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PreSalesTarget> GetByEmployeeNameAsync(string employeeName)
        {
            return await _repository.GetByEmployeeNameAsync(employeeName);
        }

        public async Task AddAsync(PreSalesTarget entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(PreSalesTarget entity)
        {
            await _repository.UpdateAsync(entity);
        }

    }
}