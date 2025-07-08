using FluentValidation;
using MinimalApis25.Models;

namespace MinimalApis25.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.FirstName)
        .NotEmpty()
        .WithMessage("First name can't be empty")
        .MaximumLength(30)
        .WithMessage("First name can't exceed 30 characters");

        RuleFor(p => p.LastName)
       .NotEmpty()
       .WithMessage("Last name can't be empty")
       .MaximumLength(30)
       .WithMessage("Last name can't exceed 30 characters");
    }
}
