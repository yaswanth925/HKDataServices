using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service;

public interface IUpdateTrackingStatusService
{
    Task<UpdateTrackingStatus> CreateAsync(UpdateTrackingStatusFormDto dto, CancellationToken ct = default);
    Task<List<UpdateTrackingStatus>> GetAllAsync(CancellationToken ct = default);
    Task<UpdateTrackingStatus?> GetByAwbNumberAsync(string awbNumber, CancellationToken ct = default);
    Task<UpdateTrackingStatus?> GetByIDAsync(Guid id, CancellationToken ct = default);
    Task<List<UpdateTrackingStatus>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct);
    Task<UpdateTrackingStatus?> UpdateAsync(string awbNumber, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default);
    Task<UpdateTrackingStatus?> UpdateAsync(Guid id, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default);
}