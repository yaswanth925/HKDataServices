using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;


namespace HKDataServices.Service
{
    public class PreSalesTargetService : IPreSalesTargetService
    {
        private readonly IPreSalesTargetRepository _repository;

        public PreSalesTargetService(IPreSalesTargetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PreSalesTarget>> GetByEmployeeNameAsync(string employeeName)
        {
            return await _repository.GetByEmployeeNameAsync(employeeName);
        }

        public async Task AddAsync(PreSalesTargetDto dto)
        {
            var target = new PreSalesTarget
            {
                EmployeeName = dto.EmployeeName,
                MonthandYear = dto.MonthandYear,
                TargetYear = dto.TargetYear,
                PreSalesVisit = dto.PreSalesVisit,
                PreSalesActivity = dto.PreSalesActivity,
                PostSalesService = dto.PostSalesService,
                Createdby = dto.Createdby,
                Created = DateTime.UtcNow
            };

            await _repository.AddAsync(target);
        }
    }
}
