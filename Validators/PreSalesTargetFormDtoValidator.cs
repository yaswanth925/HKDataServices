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

            RuleFor(x => x.MonthandYear)
                .NotEmpty().WithMessage("Month and Year is required.")
                .Matches(@"^(0[1-9]|1[0-2])-(19|20)\d{2}$")
                .WithMessage("Month and Year must be in MM-YYYY format (e.g., 10-2025).");

            RuleFor(x => x.TargetYear)
                .NotEmpty().WithMessage("Target Year is required.")
                .Matches(@"^(19|20)\d{2}$").WithMessage("Invalid year format.");

            RuleFor(x => x.PreSalesVisit)
                .NotEmpty().WithMessage("Pre-Sales Visit target is required.")
                .Matches(@"^\d+$").WithMessage("Pre-Sales Visit must be a numeric value.");

            RuleFor(x => x.PreSalesActivity)
                .NotEmpty().WithMessage("Pre-Sales Activity target is required.")
                .Matches(@"^\d+$").WithMessage("Pre-Sales Activity must be a numeric value.");

            RuleFor(x => x.PostSalesService)
                .NotEmpty().WithMessage("Post-Sales Service target is required.")
                .Matches(@"^\d+$").WithMessage("Post-Sales Service must be a numeric value.");

            RuleFor(x => x.Createdby)
                .NotEmpty().WithMessage("Created by is required.")
                .MaximumLength(50).WithMessage("Created by cannot exceed 255 characters.");

            RuleFor(x => x.Created)
                .NotNull().WithMessage("Created date is required.");

            RuleFor(x => x.Modifiedby)
                .MaximumLength(50).WithMessage("Modified by cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Modifiedby));

        }
    }
}
