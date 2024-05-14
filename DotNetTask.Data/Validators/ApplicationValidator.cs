using DotNetTask.Data.Models;
using FluentValidation;

namespace DotNetTask.Data.Validators
{
    public class ApplicationValidator : AbstractValidator<ApplicationRequest>
    {
        public ApplicationValidator()
        {
            RuleFor(model => model.FirstName).NotNull().NotEmpty();
            RuleFor(model => model.LastName).NotNull().NotEmpty();
            RuleFor(model => model.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
