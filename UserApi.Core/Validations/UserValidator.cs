using FluentValidation;
using UserApi.Core.Models;

namespace UserApi.Core.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email).NotEmpty();
    }
}
