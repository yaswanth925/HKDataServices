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
                .NotEmpty().WithMessage(_messages.DealerNameEmpty ?? "Dealer Name is required.")
                .MaximumLength(255).WithMessage(_messages.DealerNameMax ?? "Dealer Name cannot exceed 255 characters.");
            RuleFor(x => x.DealerCode)
                          .NotEmpty().WithMessage("Sales is required."); ;

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage(_messages.CustomerNameEmpty ?? "Customer Name is required.")
                .MaximumLength(255).WithMessage(_messages.CustomerNameMax ?? "Customer Name cannot exceed 255 characters.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_messages.MobileNumberEmpty ?? "Mobile number is required.")
                .Matches(@"^\+?\d{7,15}$").WithMessage(_messages.MobileNumberInvalid ?? "Mobile number format is invalid.");

            RuleFor(x => x.GSTNumber)
                .NotEmpty().WithMessage(_messages.GSTNumberEmpty ?? "GST number is required.")
                .Matches(@"^[0-9A-Z]{15}$").WithMessage(_messages.GSTNumberInvalid ?? "GST number format is invalid.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage(_messages.PincodeEmpty ?? "Pin Code is required.")
                .Matches(@"^\d+$").WithMessage(_messages.PincodeEmpty ?? "Pin Code must be a numeric value.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(_messages.CityEmpty ?? "City is required.")
                .MaximumLength(100).WithMessage(_messages.CityEmpty ?? "City must be a text.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(_messages.StateEmpty ?? "State is required.")
                .MaximumLength(100).WithMessage(_messages.StateEmpty ?? "State must be a text.");

            RuleFor(x => x.Sales)
                .NotEmpty().WithMessage(_messages.SalesEmpty.ToString() ?? "Sales is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(string.IsNullOrWhiteSpace(_messages.DateEmpty?.ToString()) ? "Date is required." : _messages.DateEmpty.ToString())
                .Must(BeAValidDate).WithMessage(string.IsNullOrWhiteSpace(_messages.DateInvalid.ToString()) ? "Date is invalid." : _messages.DateInvalid.ToString());

            RuleFor(x => x.FileData)
                .NotNull().WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                .Must(BeAValidFile).WithMessage(_messages.FileDataMax ?? "File size cannot exceed 5 MB.");

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

        private bool BeAValidDate(DateTime? date)
        {
            if (!date.HasValue)
                return false;
            return date.Value.Year >= 1900 && date.Value.Year <= 2100;
        }
    }
}
