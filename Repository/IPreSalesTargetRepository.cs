
using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IPreSalesTargetRepository
    {
        Task<IEnumerable<PreSalesTarget>> GetByEmployeeNameAsync(string employeeName);
        Task AddAsync(PreSalesTarget target);
    }
}
