using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPostSalesServiceService
    {
        Task<IEnumerable<PostSalesServiceDto>> GetAllAsync(CancellationToken ct);
        Task<PostSalesServiceDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<PostSalesServiceDto> CreateAsync(PostSalesServiceDto dto, CancellationToken ct);
        Task<bool> UpdateAsync(Guid id, PostSalesServiceDto dto, CancellationToken ct);
    }
}
