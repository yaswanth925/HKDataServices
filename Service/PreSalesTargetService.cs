using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;


namespace HKDataServices.Service
{
    public class PreSalesTargetService : IPreSalesTargetService
    {
        private readonly IPreSalesTargetRepository _repository;
        private readonly ApplicationDbContext? _context;

        public PreSalesTargetService(IPreSalesTargetRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<PreSalesTargetDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);

            return entities.Select(e => new PreSalesTargetDto
            {
                TargetID = e.TargetID,
                EmployeeName = e.EmployeeName,
                MonthYear = e.MonthYear,
                TargetYear = e.TargetYear,
                PreSalesVisit = e.PreSalesVisit,
                PreSalesActivity = e.PreSalesActivity,
                PostSalesService = e.PostSalesService,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            });
        }
        

        public async Task<PreSalesTargetResponseDto> GetByEmployeeNameAsync(string employeeName, CancellationToken ct)
        {
            var e = await _repository.GetByEmployeeNameAsync(employeeName, ct);
            if (e == null) return null;

            return new PreSalesTargetResponseDto
            {
                TargetID = e.TargetID,
                EmployeeName = e.EmployeeName,
                MonthYear = e.MonthYear,
                TargetYear = e.TargetYear,
                PreSalesVisit = e.PreSalesVisit,
                PreSalesActivity = e.PreSalesActivity,
                PostSalesService = e.PostSalesService,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified
            };
        }

        public async Task AddAsync(PreSalesTarget entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(PreSalesTarget entity)
        {
            await _repository.UpdateAsync(entity);
        }

    }
}