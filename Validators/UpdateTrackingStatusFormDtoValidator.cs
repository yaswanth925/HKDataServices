using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.Extensions.Options;

namespace HKDataServices.Validators
{
    public class UpdateTrackingStatusFormDtoValidator : AbstractValidator<UpdateTrackingStatusFormDto>
    {
        private readonly ValidationMessages _messages;

        public UpdateTrackingStatusFormDtoValidator(IOptions<ValidationMessages> messagesAccessor)
        {
            _messages = messagesAccessor?.Value ?? throw new ArgumentNullException(nameof(messagesAccessor));

            RuleFor(x => x.AWBNumber)
                .NotEmpty().WithMessage(_messages.AWBNumberEmpty ?? "AWB Number is required.")
                .MaximumLength(50).WithMessage(_messages.AWBNumberMax ?? "AWB Number cannot exceed 50 characters.");

            RuleFor(x => x.StatusType)
                .NotEmpty().WithMessage(_messages.StatusTypeEmpty ?? "Status type is required.")
                .MaximumLength(50).WithMessage(_messages.StatusTypeInvalid ?? "Status type is invalid.");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage(_messages.FileNameEmpty ?? "File name is required.")
                .MaximumLength(255).WithMessage(_messages.FileNameMax ?? "File name cannot exceed 255 characters.");

            
            RuleFor(x => x.FileData)
                .NotNull().WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                .Must(fd => fd != null && fd.Length > 0)
                    .WithMessage(_messages.FileDataEmpty ?? "File data is required.")
                .Must(fd => fd == null || fd.Length <= 5 * 1024 * 1024)
                    .WithMessage("File size cannot exceed 5 MB.");

            RuleFor(x => x.Remarks)
                .MaximumLength(255).WithMessage(_messages.RemarksMaxLength ?? "Remarks cannot exceed 255 characters.");

            RuleFor(x => x.Createdby)
                .NotEmpty().WithMessage(_messages.CreatedbyEmpty ?? "Created by is required.")
                .MaximumLength(255).WithMessage("Created by cannot exceed 255 characters.");
        }
    }
}