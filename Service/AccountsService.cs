using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;

namespace HKDataServices.Service
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _repository;

        public AccountsService(IAccountsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountsDto>> GetAllAsync(CancellationToken ct)
        {
            var entities = await _repository.GetAllAsync(ct);

            return entities.Select(e => new AccountsDto
            {
                AccountID = e.AccountID,
                DealerCode = e.DealerCode,
                DealerName = e.DealerName,
                CustomerName = e.CustomerName,
                MobileNumber = e.MobileNumber,
                GSTNumber = e.GSTNumber,
                Pincode = e.Pincode,
                City = e.City,
                State = e.State,
                Sales = e.Sales,
                Date = e.Date,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified,
                FileBase64 = e.FileData != null ? Convert.ToBase64String(e.FileData) : null
            });
        }

        public async Task<AccountsDto?> GetByDealerCodeAsync(int dealerCode)
        {
            var e = await _repository.GetByDealerCodeAsync(dealerCode);
            if (e == null) return null;

            return new AccountsDto
            {
                AccountID = e.AccountID,
                DealerCode = e.DealerCode,
                DealerName = e.DealerName,
                CustomerName = e.CustomerName,
                MobileNumber = e.MobileNumber,
                GSTNumber = e.GSTNumber,
                Pincode = e.Pincode,
                City = e.City,
                State = e.State,
                Sales = e.Sales,
                Date = e.Date,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified,
                FileBase64 = e.FileData != null ? Convert.ToBase64String(e.FileData) : null
            };
        }

        public async Task<AccountsDto?> GetByDealerNameAsync(string dealerName)
        {
            var e = await _repository.GetByDealerNameAsync(dealerName);
            if (e == null) return null;

            return new AccountsDto
            {
                AccountID = e.AccountID,
                DealerCode = e.DealerCode,
                DealerName = e.DealerName,
                CustomerName = e.CustomerName,
                MobileNumber = e.MobileNumber,
                GSTNumber = e.GSTNumber,
                Pincode = e.Pincode,
                City = e.City,
                State = e.State,
                Sales = e.Sales,
                Date = e.Date,
                CreatedBy = e.CreatedBy,
                Created = e.Created,
                ModifiedBy = e.ModifiedBy,
                Modified = e.Modified,
                FileBase64 = e.FileData != null ? Convert.ToBase64String(e.FileData) : null
            };
        }

        public async Task<AccountsDto> CreateAsync(AccountsDto dto, CancellationToken ct)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            byte[]? fileBytes = null;

            if (dto.FileData != null && dto.FileData.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.FileData.CopyToAsync(ms, ct);
                fileBytes = ms.ToArray();
            }

            var entity = new Accounts
            {
                AccountID = Guid.NewGuid(),
                DealerCode = dto.DealerCode,
                DealerName = dto.DealerName,
                CustomerName = dto.CustomerName,
                MobileNumber = dto.MobileNumber,
                GSTNumber = dto.GSTNumber,
                Pincode = dto.Pincode,
                City = dto.City,
                State = dto.State,
                Sales = dto.Sales,
                Date = dto.Date,
                FileData = fileBytes,
                CreatedBy = dto.CreatedBy,
                Created = DateTime.UtcNow
            };

            await _repository.CreateAsync(entity, ct);
            await _repository.SaveChangesAsync(ct);

            dto.AccountID = entity.AccountID;
            dto.FileBase64 = fileBytes != null ? Convert.ToBase64String(fileBytes) : null;
            return dto;
        }

        public async Task<bool> UpdateAsync(int dealerCode, AccountsDto dto, CancellationToken ct)
        {
            var entity = await _repository.GetByDealerCodeAsync(dealerCode);
            if (entity == null) return false;

            entity.DealerName = dto.DealerName;
            entity.CustomerName = dto.CustomerName;
            entity.MobileNumber = dto.MobileNumber;
            entity.GSTNumber = dto.GSTNumber;
            entity.Pincode = dto.Pincode;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.Sales = dto.Sales;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.Modified = DateTime.UtcNow;

            if (dto.FileData != null && dto.FileData.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.FileData.CopyToAsync(ms, ct);
                entity.FileData = ms.ToArray();
            }

            await _repository.UpdateAsync(entity, ct);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}
