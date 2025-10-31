using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Validators
{
    public class PostSalesServiceDtoValidator : AbstractValidator<PostSalesServiceDto>
    {
        private readonly ValidationMessages _messages = new ValidationMessages();

        public PostSalesServiceDtoValidator()
        {
            RuleFor(x => x.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description cannot exceed 255 characters.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage(_messages.ImageFileEmpty ?? "Image File is required.")
                .Must(BeAValidFile).WithMessage("Image file size cannot exceed 5 MB.");

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
