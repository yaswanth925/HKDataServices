using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Validators
{
    public class AccountsDtoValidator : AbstractValidator<AccountsDto>
    {
        private readonly ValidationMessages _messages = new ValidationMessages();

        public AccountsDtoValidator()
        {
            RuleFor(x => x.DealerName)
                .NotEmpty().WithMessage("Dealer Name is required.")
                .MaximumLength(255).WithMessage("Dealer Name cannot exceed 255 characters.");
            RuleFor(x => x.DealerCode)
                          .NotEmpty().WithMessage("Sales is required."); ;


            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_messages.MobileNumberEmpty ?? "Mobile number is required.")
                .Matches(@"^\+?\d{7,15}$").WithMessage(_messages.MobileNumberInvalid ?? "Mobile number format is invalid.");

            RuleFor(x => x.GSTNumber)
                .NotEmpty().WithMessage(_messages.GSTNumberEmpty ?? "GST number is required.")
                .Matches(@"^[0-9A-Z]{15}$").WithMessage(_messages.GSTNumberInvalid ?? "GST number format is invalid.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage("Pin Code is required.")
                .Matches(@"^\d+$").WithMessage("Pin Code must be a numeric value.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must be a text.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(100).WithMessage("State must be a text.");
            RuleFor(x => x.Sales)
                .NotEmpty().WithMessage("Sales is required.");

            RuleFor(x => x.FileData)
                 .NotNull().WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                 .Must(BeAValidFile).WithMessage("File size cannot exceed 5 MB.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created By is required.")
                .MaximumLength(50).WithMessage("Created By cannot exceed 50 characters.");

            RuleFor(x => x.Created)
                .NotNull().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedBy)
                .MaximumLength(50).WithMessage("Modified By cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.ModifiedBy));

            RuleFor(x => x.Modified)
                .NotNull().WithMessage("Modified date is required.");
        }

        private bool BeAValidFile(IFormFile? file)
        {
            if (file == null)
                return false;
            return file.Length > 0 && file.Length <= 5 * 1024 * 1024;
        }
    }
}
