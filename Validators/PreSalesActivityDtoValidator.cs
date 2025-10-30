using HKDataServices.Model.DTOs;
using FluentValidation;
using HKDataServices.Model;

namespace HKDataServices.Validators
{
    public class PreSalesActivityDtoValidator : AbstractValidator<PreSalesActivityDto>
    {
        private readonly ValidationMessages _messages = new ValidationMessages();

        public PreSalesActivityDtoValidator()
        {
            RuleFor(x => x.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description cannot exceed 255 characters.");

            RuleFor(x => x.FileData)
                .NotNull().WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                .Must(BeAValidFile).WithMessage("File size cannot exceed 5 MB.");

            RuleFor(x => x.PoValue)
                .NotEmpty().WithMessage("PO Value is required.")
                .Matches(@"^\d+$").WithMessage("PO Value must be numeric.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage(_messages.PhotoUploadEmpty ?? "Photo upload is required.")
                .Must(BeAValidFile).WithMessage("Photo file size cannot exceed 5 MB.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.")
                .MaximumLength(50).WithMessage("CreatedBy cannot exceed 50 characters.");
        }

        private bool BeAValidFile(byte[] arg)
        {
            throw new NotImplementedException();
        }

        private bool BeAValidFile(IFormFile? file)
        {
            if (file == null)
                return false; 
            return file.Length > 0 && file.Length <= 5 * 1024 * 1024; 
        }
}
}