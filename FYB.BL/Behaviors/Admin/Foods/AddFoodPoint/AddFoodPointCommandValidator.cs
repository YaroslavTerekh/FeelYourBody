using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodPoint;

public class AddFoodPointCommandValidator : AbstractValidator<AddFoodPointCommand>
{
    public AddFoodPointCommandValidator()
    {
        RuleFor(t => t.Title)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TitleTooShort)
            .MaximumLength(100)
            .WithMessage(ValidationMessages.TitleTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.TitleRequired);

        RuleFor(t => t.Description)
            .MinimumLength(50)
            .WithMessage(ValidationMessages.DescriptionTooShort)
            .MaximumLength(200)
            .WithMessage(ValidationMessages.DescriptionTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.DescriptionRequired);

        RuleFor(t => t.PortionMass)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.WrongPortionMass)
            .NotEmpty()
            .WithMessage(ValidationMessages.WrongPortionMass);
    }
}
