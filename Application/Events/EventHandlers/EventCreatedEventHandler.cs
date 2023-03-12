using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Events.EventHandlers;

public class EventCreatedEventHandler : INotificationHandler<EventCreatedEvent>
{
    private readonly IDelayedMessageService _messageService;

    public EventCreatedEventHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task Handle(EventCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _messageService.CreateDelayedMessageAsync(
            notification.Message, 
            notification.ReceiverId, 
            notification.TimeToRemind);
    }
}