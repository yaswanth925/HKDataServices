using HKDataServices.Model.DTOs;
using HKDataServices.Repository;

namespace HKDataServices.Service
{
    public class PreSalesTargetListService : IPreSalesTargetListService
    {
        private readonly IPreSalesTargetListRepository _repo;

        public PreSalesTargetListService(IPreSalesTargetListRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PreSalesTargetListDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);

            return entities.Select(e => new PreSalesTargetListDto
            {
                ListID = e.ListID,
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
    }
}
