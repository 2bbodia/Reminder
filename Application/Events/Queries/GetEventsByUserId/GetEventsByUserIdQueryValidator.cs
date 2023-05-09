using FluentValidation;

namespace Application.Events.Queries.GetEventsByUserId;

public class GetEventsByUserIdQueryValidator : AbstractValidator<GetEventsByUserIdQuery>
{
    public GetEventsByUserIdQueryValidator()
    {
        RuleFor(q => q.UserId).NotEmpty().WithMessage("Something wrong with your telegram account");
        RuleFor(q => q.Date).NotEmpty().WithMessage("Invalid date value");
    }
}
