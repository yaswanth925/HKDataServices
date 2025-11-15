using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.Extensions.Options;

namespace HKDataServices.Validators
{
    public class PreSalesActivityDtoValidator : AbstractValidator<PreSalesActivityDto>
    {
        private readonly ValidationMessages _messages;

        public PreSalesActivityDtoValidator(IOptions<ValidationMessages> messages)
        {
            _messages = messages.Value;
            RuleFor(x => x.ActivityType)
                .NotEmpty().WithMessage(_messages.ActivityTypeEmpty ?? "Activity Type is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(_messages.DescriptionEmpty ?? "Description is required.")
                .MaximumLength(255).WithMessage(_messages.DescriptionMax ?? "Description cannot exceed 255 characters.");

            RuleFor(x => x.PoValue)
                .NotEmpty().WithMessage(_messages.PoValueEmpty ?? "PO Value is required.")
                .Must(v => int.TryParse(v, out var num) && num > 0)
                .WithMessage(_messages.PoValueMax ?? "PO Value must be a number greater than zero.");

            RuleFor(x => x.FileData)
                .NotNull().WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                .Must(BeAValidFile).WithMessage(_messages.FileDataMax ?? "File size cannot exceed 5 MB.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage(_messages.ImageFileEmpty ?? "Image File is required.")
                .Must(BeAValidFile).WithMessage(_messages.ImageFileMax ?? "Image file size cannot exceed 5 MB.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage(_messages.CreatedByEmpty ?? "CreatedBy is required.")
                .MaximumLength(50).WithMessage(_messages.CreatedByMax ?? "CreatedBy cannot exceed 50 characters.");
        }

        private bool BeAValidFile(IFormFile? file)
        {
            if (file == null) return false;

            return file.Length > 0 && file.Length <= (5 * 1024 * 1024);
        }
    }
}