using FluentValidation;

namespace StockTraceSystem.Application.Feature.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.UserForLoginDto.Email).NotEmpty().WithMessage("Lütfen email alanını doldurunuz.");
                                                 //.EmailAddress().WithMessage("Lütfen uygun formatta mail giriniz.")

            RuleFor(l => l.UserForLoginDto.Password).NotEmpty().WithMessage("Lütfen şifre alanını doldurunuz.");
        }
    }
}