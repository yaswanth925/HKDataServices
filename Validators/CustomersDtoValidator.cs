using FluentValidation;
using HKDataServices.Model.DTOs;
using HKDataServices.Model;

namespace HKDataServices.Validators
{
    public class CustomersDtoValidator : AbstractValidator<CustomersDto>
    {
        private readonly ValidationMessages _messages = new ValidationMessages();

        public CustomersDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer Name is required.")
                .MaximumLength(255).WithMessage("Customer Name cannot exceed 255 characters.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_messages.MobileNumberEmpty ?? "Mobile number is required.")
                .Matches(@"^\+?\d{7,15}$").WithMessage(_messages.MobileNumberInvalid ?? "Mobile number format is invalid.");

            RuleFor(x => x.EmailId)
                .NotEmpty().WithMessage(_messages.EmailEmpty ?? "Email is required.")
                .EmailAddress().WithMessage(_messages.EmailInvalid ?? "Email is not valid.");

            RuleFor(x => x.GSTNumber)
                .NotEmpty().WithMessage(_messages.GSTNumberEmpty ?? "GST number is required.")
                .Matches(@"^[0-9A-Z]{15}$").WithMessage(_messages.GSTNumberInvalid ?? "GST number format is invalid.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address must be a text.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage("Pin Code is required.")
                .Matches(@"^\d+$").WithMessage("Pin Code must be a numeric value.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must be a text.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(100).WithMessage("State must be a text.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be a text.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage(_messages.ImageFileEmpty ?? "Image File is required.")
                .Must(fd => fd != null && fd.Length > 0)
                    .WithMessage(_messages.ImageFileEmpty ?? "Image File is required.")
                .Must(fd => fd == null || fd.Length <= 5 * 1024 * 1024)
                    .WithMessage("File size cannot exceed 5 MB.");

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
    }
}
