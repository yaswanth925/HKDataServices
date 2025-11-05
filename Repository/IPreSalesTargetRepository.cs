using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IPreSalesTargetRepository
    {
        Task<IEnumerable<PreSalesTarget>> GetAllAsync();
        Task<PreSalesTarget> GetByEmployeeNameAsync(String employeeName);
        Task AddAsync(PreSalesTarget entity);
        Task UpdateAsync(PreSalesTarget entity);
    }
}