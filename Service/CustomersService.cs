using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<CustomersDto>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Customers
                .Select(c => new CustomersDto
                {
                    CustomerID = c.CustomerID,
                    CustomerName = c.CustomerName,
                    MobileNumber = c.MobileNumber,
                    EmailId = c.EmailId,
                    GSTNumber = c.GSTNumber,
                    Address = c.Address,
                    Pincode = c.Pincode,
                    City = c.City,
                    State = c.State,
                    CreatedBy = c.CreatedBy,
                    ModifiedBy = c.ModifiedBy,
                })
                .ToListAsync(ct);
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
        public async Task UpdateAsync(CustomersDto dto, CancellationToken ct)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerName == dto.CustomerName, ct);

            if (customer == null)
                throw new Exception("Customer not found.");

            customer.CustomerName = dto.CustomerName;
            customer.MobileNumber = dto.MobileNumber;
            customer.EmailId = dto.EmailId;
            customer.GSTNumber = dto.GSTNumber;
            customer.Address = dto.Address;
            customer.Pincode = dto.Pincode;
            customer.City = dto.City;
            customer.State = dto.State;
            customer.ModifiedBy = dto.ModifiedBy;
           
            if (!string.IsNullOrEmpty(dto.Modified))
                customer.Modified = DateTime.Parse(dto.Modified);
            else
                customer.Modified = null;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<IActionResult> PatchAsync(Guid id, CustomersPatchDto dto, CancellationToken ct)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id, ct);
            if (customer == null)
                return new NotFoundResult();
            if (!string.IsNullOrEmpty(dto.MobileNumber))
                customer.MobileNumber = dto.MobileNumber;
            if (!string.IsNullOrEmpty(dto.EmailId))
                customer.EmailId = dto.EmailId;
            if (!string.IsNullOrEmpty(dto.Address))
                customer.Address = dto.Address;
            if (!string.IsNullOrEmpty(dto.Pincode))
                customer.Pincode = dto.Pincode;
            if (!string.IsNullOrEmpty(dto.City))
                customer.City = dto.City;
            if (!string.IsNullOrEmpty(dto.State))
                customer.State = dto.State;
            if (!string.IsNullOrEmpty(dto.ModifiedBy))
                customer.ModifiedBy = dto.ModifiedBy;
            if (!string.IsNullOrEmpty(dto.Modified))
                customer.Modified = DateTime.Parse(dto.Modified);

            //if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            //{
            //    if (!dto.ImageFile.ContentType.StartsWith("image/"))
            //        return new BadRequestObjectResult("Only image files are allowed for ImageFile.");
            //    if (dto.ImageFile.Length > 5 * 1024 * 1024)
            //        return new BadRequestObjectResult("ImageFile is too large.");

            //    using (var ms = new MemoryStream())
            //    {
            //        await dto.ImageFile.CopyToAsync(ms, ct);
            //        customer.ImageFile = ms.ToArray();
            //    }
            //}

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(ct);

            return new OkResult();
        }
    }
}
