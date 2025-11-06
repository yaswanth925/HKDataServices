using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IPreSalesTargetListRepository
    {
        Task<IEnumerable<PreSalesTargetList>> GetAllAsync(CancellationToken ct);
    }
}
