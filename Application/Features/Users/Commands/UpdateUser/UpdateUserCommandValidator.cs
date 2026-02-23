using FluentValidation;

namespace Senswise.UserService.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Kullanıcı ID'si zorunludur.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı zorunludur.")
            .MaximumLength(100).WithMessage("Ad 100 karakteri geçemez.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad alanı zorunludur.")
            .MaximumLength(100).WithMessage("Soyad 100 karakteri geçemez.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(255).WithMessage("E-posta 255 karakteri geçemez.");

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Adres 500 karakteri geçemez.")
            .When(x => !string.IsNullOrWhiteSpace(x.Address));
    }
}
