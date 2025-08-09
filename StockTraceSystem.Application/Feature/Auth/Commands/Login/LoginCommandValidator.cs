using FluentValidation;

namespace StockTraceSystem.Application.Feature.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserForLoginDto.Email).NotEmpty().EmailAddress().WithMessage("aa").MinimumLength(4).WithMessage("bb");
            RuleFor(c => c.UserForLoginDto.Password).NotEmpty();
        }
    }
}