using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using Microsoft.Extensions.Options;
using FluentValidation;

namespace HKDataServices.Validators
{
    public class UsersFormDtoValidator : AbstractValidator<UsersFormDto>
    {
        private readonly ValidationMessages _messages;


        public UsersFormDtoValidator(IOptions<ValidationMessages> messagesAccessor)
        {
            _messages = messagesAccessor?.Value ?? throw new ArgumentNullException(nameof(messagesAccessor));

            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage(_messages.FirstNameEmpty ?? "First name is required.")
               .MaximumLength(50).WithMessage(_messages.FirstNameMax ?? "First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(_messages.LastNameEmpty?? "Last name is required.")
                .MaximumLength(50).WithMessage(_messages.LastNameMax ?? "Last name must be 50 characters or fewer.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage(_messages.MobileNumberEmpty ?? "Mobile number is required.")
                .Matches(@"^\+?\d{7,15}$").WithMessage(_messages.MobileNumberInvalid ?? "Mobile number format is invalid.");

            RuleFor(x => x.EmailID)
                .NotEmpty().WithMessage(_messages.EmailEmpty ?? "Email is required.")
                .EmailAddress().WithMessage(_messages.EmailInvalid ?? "Email is not valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_messages.PasswordEmpty ?? "Password is required.")
                .MinimumLength(6).WithMessage(_messages.PasswordMinLength ?? "Password must be at least 6 characters.");

            RuleFor(x => x.Createdby)
                .NotEmpty().WithMessage(_messages.CreatedbyEmpty ?? "Createdby is required.");
        }
    }
}
