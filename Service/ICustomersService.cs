using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Service
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customers>> GetByCustomerNameAsync(string customerName);
        Task<IEnumerable<Customers>> GetByMobileNumberAsync(string mobileNumber);
        Task<IEnumerable<Customers>> GetByEmailIdAsync(string emailId);
        Task CreateAsync(CustomersDto dto, CancellationToken ct);
        Task<List<CustomersDto>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(CustomersDto dto, CancellationToken ct);
        Task<IActionResult> PatchAsync(Guid id, CustomersPatchDto dto, CancellationToken ct);
    }
}
