using DotNetTask.Data.Models;
using FluentValidation;

namespace DotNetTask.Data.Validators
{
    public class CreateProgramValidator : AbstractValidator<CreateProgramRequest>
    {
        public CreateProgramValidator()
        {
            RuleFor(model => model.ProgramTitle).NotNull().NotEmpty();
            RuleFor(model => model.ProgrammeDescription).NotNull().NotEmpty();
        }
    }

    public class UpdateProgramValidator : AbstractValidator<UpdateProgramRequest>
    {
        public UpdateProgramValidator()
        {
            RuleFor(model => model.ProgramTitle).NotNull().NotEmpty();
            RuleFor(model => model.ProgrammeDescription).NotNull().NotEmpty();
        }
    }
}
