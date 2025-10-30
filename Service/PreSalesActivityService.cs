using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Service
{
    public class PreSalesActivityService : IPreSalesActivityService
    {
        private readonly IPreSalesActivityRepository _repo;
        private readonly ApplicationDbContext _context;

        public PreSalesActivityService(IPreSalesActivityRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<PreSalesActivityDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);

            return entities.Select(e => new PreSalesActivityDto
            {
                PreSalesActivityID = e.PreSalesActivityID,
                CustomerID = e.CustomerID,
                ActivityType = e.ActivityType,
                Description = e.Description,
                PoValue = e.PoValue,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            });
        }

        public async Task<PreSalesActivityDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e == null) return null;

            return new PreSalesActivityDto
            {
                PreSalesActivityID = e.PreSalesActivityID,
                CustomerID = e.CustomerID,
                ActivityType = e.ActivityType,
                Description = e.Description,
                PoValue = e.PoValue,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            };
        }

        public async Task<PreSalesActivityDto> CreateAsync(PreSalesActivityDto dto, CancellationToken ct)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.FileData == null || dto.FileData.Length == 0)
                throw new ArgumentException("FileData is required and cannot be empty.");

            if (dto.ImageFile == null || dto.ImageFile.Length == 0)
                throw new ArgumentException("ImageFile is required and cannot be empty.");

            byte[] fileBytes;
            await using (var ms = new MemoryStream())
            {
                await dto.FileData.CopyToAsync(ms, ct);
                fileBytes = ms.ToArray();
            }

            byte[] imageBytes;
            await using (var ms = new MemoryStream())
            {
                await dto.ImageFile.CopyToAsync(ms, ct);
                imageBytes = ms.ToArray();
            }

            
            var customerExists = await _context.Customers
                .AnyAsync(c => c.CustomerID == dto.CustomerID, ct);

            if (!customerExists)
                throw new InvalidOperationException("Invalid CustomerID. Please create or select a valid customer before adding a pre-sales activity.");

            var entity = new PreSalesActivity
            {
                PreSalesActivityID = Guid.NewGuid(),
                CustomerID = dto.CustomerID,
                ActivityType = dto.ActivityType ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                PoValue = dto.PoValue,
                FileData = fileBytes,
                ImageFile = imageBytes,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.UtcNow
            };

            await _repo.CreateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return new PreSalesActivityDto
            {
                PreSalesActivityID = entity.PreSalesActivityID,
                CustomerID = entity.CustomerID,
                ActivityType = entity.ActivityType,
                Description = entity.Description,
                PoValue = entity.PoValue,
                CreatedBy = entity.CreatedBy,
                Created = entity.Created
            };
        }

        public async Task<bool> UpdateAsync(Guid id, PreSalesActivityDto dto, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null)
                return false;

            entity.CustomerID = dto.CustomerID;
            entity.ActivityType = dto.ActivityType ?? entity.ActivityType;
            entity.Description = dto.Description ?? entity.Description;
            entity.PoValue = dto.PoValue ?? entity.PoValue;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.Modified = DateTime.UtcNow;

            _repo.Update(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
