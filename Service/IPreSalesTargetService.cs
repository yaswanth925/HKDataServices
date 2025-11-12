using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPreSalesTargetService
    {
        Task<IEnumerable<PreSalesTargetDto>> GetAllAsync(CancellationToken ct);
        Task<PreSalesTargetResponseDto> GetByEmployeeNameAsync(string employeeName, CancellationToken ct);
        Task AddAsync(PreSalesTarget entity);
        Task UpdateAsync(PreSalesTarget entity);
    }
}