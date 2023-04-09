using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Events.Commands.DeleteEvent;

 public record DeleteEventCommand(Guid EventId) : IRequest;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IApplicationDbContext _db;

    public DeleteEventCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var userEvent = await _db.Events.FindAsync(request.EventId) ?? throw new Exception();

        _db.Events.Remove(userEvent);
        
        userEvent.AddDomainEvent(new EventDeletedEvent(userEvent.Id));

        await _db.SaveChangesAsync(cancellationToken);
    }
}
