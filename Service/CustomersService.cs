using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Service
{
    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext _context;

        public CustomersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetByCustomerNameAsync(string customerName)
        {
            return await _context.Customers
                .Where(c => c.CustomerName.Contains(customerName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Customers>> GetByMobileNumberAsync(string mobileNumber)
        {
            return await _context.Customers
                .Where(c => c.MobileNumber == mobileNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customers>> GetByEmailIdAsync(string emailId)
        {
            return await _context.Customers
                .Where(c => c.EmailId == emailId)
                .ToListAsync();
        }

        public async Task CreateAsync(CustomersDto dto, CancellationToken ct)
        {
            byte[]? fileBytes = null;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.ImageFile.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
            }

            if (!dto.ImageFile.ContentType.StartsWith("image/"))
                throw new ArgumentException("Only image files are allowed for ImageFile.");

            if (dto.ImageFile.Length > 5 * 1024 * 1024)
                throw new ArgumentException("ImageFile is too large.");

            var entity = new Customers
            {
                CustomerID = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                MobileNumber = dto.MobileNumber,
                EmailId = dto.EmailId,
                GSTNumber = dto.GSTNumber,
                Address = dto.Address,
                Pincode = dto.Pincode,
                City = dto.City,
                State = dto.State,
                Description = dto.Description,
                ImageFile = fileBytes,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.UtcNow
            };

            await _context.Customers.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public Task<IEnumerable<object>> GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
