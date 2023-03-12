using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System.Text;

namespace Application.Events.Commands.CreateEvent;

public record CreateEventCommand(
    long UserId,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    DateTime RemindAt) : IRequest<Guid>;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FindAsync(request.UserId) ?? throw new Exception();

        var userEvent = _mapper.Map<Event>(request);
        StringBuilder sb = new();
        sb.Append($"Нагадування:{Environment.NewLine}")
            .Append($"Назва події: {userEvent.Title}{Environment.NewLine}")
            .Append($"Опис події: {userEvent.Description}{Environment.NewLine}")
            .Append($"Початок події: {userEvent.StartDate}{Environment.NewLine}")
            .Append($"Кінець події: {userEvent.EndDate}{Environment.NewLine}");

        userEvent.AddDomainEvent(new EventCreatedEvent(user.Id, sb.ToString(), request.RemindAt));

        await _db.Events.AddAsync(userEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return userEvent.Id;

    }
}