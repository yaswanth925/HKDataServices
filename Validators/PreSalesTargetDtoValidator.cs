using FluentValidation;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Validators
{
    public class PreSalesTargetDtoValidator : AbstractValidator<PreSalesTargetDto>
    {
        public PreSalesTargetDtoValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage("Employee Name is required.")
                .MaximumLength(100).WithMessage("Employee Name cannot exceed 255 characters.");

            RuleFor(x => x.MonthYear)
            .NotEmpty().WithMessage("Month & Year is required.")
            .Must(BeAValidMonthYear).WithMessage("Month & Year must be between 1900 and 2100.");

            RuleFor(x => x.TargetYear)
                .NotEmpty().WithMessage("Target Year is required.")
                .InclusiveBetween(1900, 2100)
                .WithMessage("Target Year must be between 1900 and 2100.");

            RuleFor(x => x.PreSalesVisit)
                .NotEmpty().WithMessage("Pre-Sales Visit target is required.");
                

            RuleFor(x => x.PreSalesActivity)
                .NotEmpty().WithMessage("Pre Sales Activity is required.");

            RuleFor(x => x.PostSalesService)
                .NotEmpty().WithMessage("Post Sales Service is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created By is required.")
                .MaximumLength(50).WithMessage("Created By cannot exceed 255 characters.");

            RuleFor(x => x.Created)
                .NotNull().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedBy)
                .MaximumLength(50).WithMessage("Modified By cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.ModifiedBy));

            RuleFor(x => x.Modified)
                .NotNull().WithMessage("Modified date is required.");
        }
        private bool BeAValidMonthYear(DateTime date)
        {
            return date.Year >= 1900 && date.Year <= 2100;
        }
    }
}
