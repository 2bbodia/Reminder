using FluentValidation;

namespace Application.Events.Queries.GetEventIfExistInParticularTime;

public class GetEventIfExistInParticularTimeQueryValidator : AbstractValidator<GetEventIfExistInParticularTimeQuery>
{
    public GetEventIfExistInParticularTimeQueryValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().WithMessage("Something wrong with your telegram account");
        RuleFor(e => e.StartDate).NotEmpty().WithMessage("Invalid date value");
        RuleFor(e => e.EndDate).NotEmpty().WithMessage("Invalid date value")
            .GreaterThan(e => e.StartDate).WithMessage("End date must be greater than start date");
    }
}
