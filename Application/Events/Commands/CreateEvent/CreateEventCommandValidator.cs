using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Events.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.UserId).NotEmpty().WithMessage("Щось не так з вашим аккаунтом");
        RuleFor(e => e.Title).NotEmpty().WithMessage("Заголовок не може бути порожнім");
        RuleFor(e => e.Description).NotEmpty().WithMessage("Опис не може бути порожнім");
        RuleFor(e => e.StartDate).NotEmpty().WithMessage("Не правильне значення дати");
        RuleFor(e => e.EndDate).NotEmpty().WithMessage("Не правильне значення дати")
            .GreaterThan(e => e.StartDate).WithMessage("Кінцева дата має бути більша за початкову");
        RuleFor(e => e.RemindAt).NotEmpty().WithMessage("Не правильне значення дати");
        RuleFor(e => e.Importance).NotEmpty().WithMessage("Значення важливості не може бути порожнім");

    }
}
