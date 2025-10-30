using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPreSalesActivityService
    {
        Task<IEnumerable<PreSalesActivityDto>> GetAllAsync(CancellationToken ct);
        Task<PreSalesActivityDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<PreSalesActivityDto> CreateAsync(PreSalesActivityDto dto, CancellationToken ct);
        Task<bool> UpdateAsync(Guid id, PreSalesActivityDto dto, CancellationToken ct);
    }
}
