using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPreSalesTargetService
    {
        Task<IEnumerable<PreSalesTarget>> GetAllAsync();
        Task<PreSalesTarget> GetByEmployeeNameAsync(string employeeName);
        Task AddAsync(PreSalesTarget entity);
        Task UpdateAsync(PreSalesTarget entity);
    }
}