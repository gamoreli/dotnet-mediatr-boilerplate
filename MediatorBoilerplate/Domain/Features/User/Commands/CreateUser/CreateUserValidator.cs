using FluentValidation;

namespace MediatorBoilerplate.Domain.Features.User.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserMessage>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Name.Trim())
                .NotEmpty()
                .WithMessage("Name required");

            RuleFor(user => user.Email.Trim())
                .NotEmpty()
                .WithMessage("Email required");
        }
    }
}