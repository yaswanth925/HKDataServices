using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Service
{
    public class PostSalesServiceService : IPostSalesServiceService
    {
        private readonly IPostSalesServiceRepository _repo;
        private readonly ApplicationDbContext _context;

        public PostSalesServiceService(IPostSalesServiceRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<PostSalesServiceDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);

            return entities.Select(e => new PostSalesServiceDto
            {
                PostSalesServiceID = e.PostSalesServiceID,
                CustomerID = e.CustomerID,                
                Description = e.Description,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            });
        }
        public async Task<PostSalesServiceDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e == null) return null;

            return new PostSalesServiceDto
            {
                PostSalesServiceID = e.PostSalesServiceID,
                CustomerID = e.CustomerID,              
                Description = e.Description,               
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            };
        }
        public async Task<PostSalesServiceDto> CreateAsync(PostSalesServiceDto dto, CancellationToken ct)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.ImageFile.Length > 5 * 1024 * 1024)
                throw new ArgumentException("ImageFile is too large.");

            if (!dto.ImageFile.ContentType.StartsWith("image/"))
                throw new ArgumentException("Only image files are allowed for ImageFile.");
            
            byte[] imageBytes;
            await using (var ms = new MemoryStream())
            {
                await dto.ImageFile.CopyToAsync(ms, ct);
                imageBytes = ms.ToArray();
            }

            var customer = await _context.Customers
         .FirstOrDefaultAsync(c => c.CustomerID == dto.CustomerID, ct);

            if (customer == null)
                throw new InvalidOperationException("Invalid CustomerID. Please create or select a valid customer before adding a Post Sales Service.");

            var entity = new PostSalesService
            {
                PostSalesServiceID = Guid.NewGuid(),
                CustomerID = dto.CustomerID,
                Description = dto.Description ?? string.Empty,
                ImageFile = imageBytes,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.UtcNow
            };

            await _repo.CreateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return new PostSalesServiceDto
            {
                PostSalesServiceID = entity.PostSalesServiceID,
                CustomerID = entity.CustomerID,            
                Description = entity.Description,               
                CreatedBy = entity.CreatedBy,
                Created = entity.Created
            };

        }

        Task<PostSalesServiceDto?> IPostSalesServiceService.GetByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid id, PostSalesServiceDto dto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
