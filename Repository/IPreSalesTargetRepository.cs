using HKDataServices.Model;
using HKDataServices.Model.DTOs;
namespace HKDataServices.Repository
{
    public interface IPreSalesTargetRepository
    {
        Task<IEnumerable<PreSalesTarget>> GetAllAsync(CancellationToken ct);
        Task<PreSalesTarget> GetByEmployeeNameAsync(String employeeName, CancellationToken ct);
        Task AddAsync(PreSalesTarget entity);
        Task UpdateAsync(PreSalesTarget entity);
    }
}