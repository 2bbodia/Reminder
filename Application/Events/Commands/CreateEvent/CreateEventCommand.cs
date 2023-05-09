using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application.Events.Commands.CreateEvent;

public record CreateEventCommand(
    long UserId,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    DateTime RemindAt,
    string Importance) : IRequest<Guid>;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IApplicationDbContext db, IMapper mapper, IValidator<CreateEventCommand> validator)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FindAsync(request.UserId) ?? throw new Exception();
        var eventImportance = await _db
            .EventImportances
            .FirstOrDefaultAsync(e => e.Name.Equals(request.Importance));
        var userEvent = _mapper.Map<Event>(request);
        if(eventImportance != null)
        {
            userEvent.EventImportanceId = eventImportance.Id;
        }
        await _db.Events.AddAsync(userEvent);

        userEvent.AddDomainEvent(
            new EventCreatedEvent(
                userEvent,
                request.RemindAt ));

        await _db.SaveChangesAsync(cancellationToken);

        return userEvent.Id;

    }
}