using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Events.EventHandlers;

public class EventDeletedEventHandler : INotificationHandler<EventDeletedEvent>
{
    private readonly IEventReminderService _eventReminderService;

    public EventDeletedEventHandler(IEventReminderService eventReminderService)
    {
        _eventReminderService = eventReminderService;
    }

    public async Task Handle(EventDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _eventReminderService.CancelRemindingAboutEventAsync(
            notification.EventId);
    }
}
