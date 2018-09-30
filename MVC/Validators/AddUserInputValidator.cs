using FluentValidation;
using MVC.Models;

namespace MVC.Validators
{
    public class AddUserInputValidator : AbstractValidator<AddUserInput>
    {
        public AddUserInputValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithName("用户姓名");
        }
    }
}
