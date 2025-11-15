using FluentValidation;
using HKDataServices.Model.DTOs;
using HKDataServices.Model;
using Microsoft.Extensions.Options;

namespace HKDataServices.Validators
{
    public class CustomersDtoValidator : AbstractValidator<CustomersDto>
    {
        private readonly ValidationMessages _messages;

        public CustomersDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage(_messages.CustomerNameEmpty ?? "Customer Name is required.")
                .MaximumLength(255).WithMessage(_messages.CustomerNameMax ?? "Customer Name cannot exceed 255 characters.");

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
                .NotEmpty().WithMessage(_messages.AddressEmpty ?? "Address is required.")
                .MaximumLength(500).WithMessage(_messages.AddressEmpty ?? "Address must be a text.");

            RuleFor(x => x.Pincode)
                .NotEmpty().WithMessage(_messages.PincodeEmpty ?? "Pin Code is required.")
                .Matches(@"^\d+$").WithMessage(_messages.PincodeEmpty ?? "Pin Code must be a numeric value.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(_messages.CityEmpty ?? "City is required.")
                .MaximumLength(100).WithMessage(_messages.CityEmpty ?? "City must be a text.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(_messages.StateEmpty ?? "State is required.")
                .MaximumLength(100).WithMessage(_messages.StateEmpty ?? "State must be a text.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(_messages.DescriptionEmpty ?? "Description is required.")
                .MaximumLength(1000).WithMessage(_messages.DescriptionEmpty ?? "Description must be a text.");

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
