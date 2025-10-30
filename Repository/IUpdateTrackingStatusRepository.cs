using HKDataServices.Model;
using HKDataServices.Service;
using System.Threading;
using System.Threading.Tasks;

namespace HKDataServices.Repository;

public interface IUpdateTrackingStatusRepository
{
    Task<List<UpdateTrackingStatus>> GetAllAsync(CancellationToken ct);
    Task<UpdateTrackingStatus> InsertAsync(UpdateTrackingStatus entity, CancellationToken ct = default);
    Task<UpdateTrackingStatus?> GetByAwbNumberAsync(string awbNumber, CancellationToken ct = default);
    Task<UpdateTrackingStatus?> GetByIDAsync(Guid id, CancellationToken ct = default);
    Task<List<UpdateTrackingStatus>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<UpdateTrackingStatus?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default);
}
