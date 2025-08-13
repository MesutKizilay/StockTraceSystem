using FluentValidation;

namespace StockTraceSystem.Application.Feature.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Lütfen ad alanını doldurunuz.")
                                     .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olabilir.");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Lütfen soyad alanını doldurunuz.")
                                    .MinimumLength(2).WithMessage("Soyad alanı en az 2 karakter olabilir.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Lütfen şifre alanını doldurunuz.")
                                    .MinimumLength(3).WithMessage("Şifre alanı en az 3 karakter olabilir.");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Lütfen email alanını doldurunuz.");
                                 //.EmailAddress().WithMessage("Lütfen uygun formatta mail giriniz.");
        }
    }
}