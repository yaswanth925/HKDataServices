using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPreSalesTargetListService
    {
        Task<IEnumerable<PreSalesTargetListDto>> GetAllAsync(CancellationToken ct);
    }
}
