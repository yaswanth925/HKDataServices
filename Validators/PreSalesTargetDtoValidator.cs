using FluentValidation;
using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Validators
{
    public class PreSalesTargetDtoValidator : AbstractValidator<PreSalesTargetDto>
    {
        private readonly ValidationMessages _messages = new ValidationMessages();
        public PreSalesTargetDtoValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage(_messages.EmployeeNameEmpty ?? "Employee Name is required.")
                .MaximumLength(100).WithMessage(_messages.EmployeeNameMax ?? "Employee Name cannot exceed 255 characters.");

            RuleFor(x => x.MonthYear)
                .NotEmpty().WithMessage(_messages.MonthYearEmpty.ToString())
                .Must(BeAValidMonthYear).WithMessage(_messages.MonthYearInvalid.ToString());

            RuleFor(x => x.TargetYear)
                .NotEmpty().WithMessage(_messages.TargetYearEmpty.ToString())
                .InclusiveBetween(1900, 2100)
                .WithMessage(_messages.TargetYearInvalid.ToString());

            RuleFor(x => x.PreSalesVisit)
                .NotEmpty().WithMessage(_messages.PreSalesVisitEmpty.ToString() ?? "Pre-Sales Visit target is required.");
                

            RuleFor(x => x.PreSalesActivity)
                .NotEmpty().WithMessage(_messages.PreSalesActivityEmpty.ToString() ?? "Pre Sales Activity is required.");

            RuleFor(x => x.PostSalesService)
                .NotEmpty().WithMessage(_messages.PostSalesServiceEmpty.ToString() ?? "Post Sales Service is required.");

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
