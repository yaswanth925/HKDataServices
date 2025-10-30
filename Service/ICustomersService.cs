using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customers>> GetByCustomerNameAsync(string customerName);
        Task<IEnumerable<Customers>> GetByMobileNumberAsync(string mobileNumber);
        Task<IEnumerable<Customers>> GetByEmailIdAsync(string emailId);
        Task CreateAsync(CustomersDto dto, CancellationToken ct);
        Task<IEnumerable<object>> GetAllAsync(CancellationToken ct);
    }
}
