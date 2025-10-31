using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;


namespace HKDataServices.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;
        private readonly IValidator<UsersFormDto> _validator;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUsersRepository repo, IValidator<UsersFormDto> validator, ILogger<UsersService> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UsersResponseDto> CreateUsersAsync(UsersFormDto dto, CancellationToken ct = default)
        {
            if (dto is null)
            {
                _logger.LogError("Received null UsersFormDto in CreateUsersAsync");
                throw new ArgumentNullException(nameof(dto));
            }

            var validationResult = await _validator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
            {
                var details = string.Join(" | ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                _logger.LogWarning("Validation failed for user creation: {Errors}", details);
                throw new ValidationException(details);
            }

            ct.ThrowIfCancellationRequested();

            dto.EmailID = dto.EmailID?.Trim().ToLowerInvariant();
            dto.MobileNumber = dto.MobileNumber?.Trim();
            dto.FirstName = dto.FirstName?.Trim();
            dto.LastName = dto.LastName?.Trim();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var entity = new Users
            {
                ID = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobileNumber = dto.MobileNumber,
                EmailID = dto.EmailID,
                Password = hashedPassword, 
                CreatedBy = dto.CreatedBy,
                Created = DateTime.UtcNow,
                ModifiedBy = dto.CreatedBy,
                Modified = DateTime.UtcNow,
                IsActive = dto.IsActive
            };

            try
            {
                var createdEntity = await _repo.InsertAsync(entity, ct);

                if (createdEntity == null)
                {
                    _logger.LogError("Repository returned null Users entity");
                    throw new InvalidOperationException("Failed to create user in repository");
                }

                return new UsersResponseDto
                {
                    ID = createdEntity.ID,
                    FirstName = createdEntity.FirstName,
                    LastName = createdEntity.LastName,
                    MobileNumber = createdEntity.MobileNumber,
                    EmailID = createdEntity.EmailID,
                    IsActive = createdEntity.IsActive,
                    Created = createdEntity.Created,
                    CreatedBy = createdEntity.CreatedBy
                };
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while creating user with EmailID {Email}", dto.EmailID);
                throw new InvalidOperationException("Database error occurred while creating the user.", dbEx);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("CreateUsersAsync cancelled for EmailID {Email}.", dto.EmailID);
                throw;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating user with EmailID {Email}.", dto.EmailID);
                throw new InvalidOperationException("An unexpected error occurred while creating the user.", ex);
            }
        }

        public Task<Users?> GetByEmailIDAsync(string EmailID, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Users?> GetByMobileNumberAsync(string MobileNumber, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}