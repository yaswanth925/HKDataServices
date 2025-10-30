using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customers>> GetAllAsync();
        Task<Customers?> GetByIdAsync(Guid id);
        Task CreateAsync(Customers entity);
        Task UpdateAsync(Customers entity);
        
    }
}
