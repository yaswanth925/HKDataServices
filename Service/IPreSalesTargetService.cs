using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IPreSalesTargetService
    {
        Task<IEnumerable<PreSalesTarget>> GetByEmployeeNameAsync(string employeeName);
        Task AddAsync(PreSalesTargetDto dto);
    }
}

