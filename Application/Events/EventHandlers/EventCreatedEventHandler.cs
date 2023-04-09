using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using System.Text;

namespace Application.Events.EventHandlers;

public class EventCreatedEventHandler : INotificationHandler<EventCreatedEvent>
{
    private readonly IEventReminderService _eventReminderService;

    public EventCreatedEventHandler(IEventReminderService eventReminderService)
    {
        _eventReminderService = eventReminderService;
    }

    public async Task Handle(EventCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userEvent = notification.Event;
        StringBuilder sb = new();
        sb.Append($"Нагадування:{Environment.NewLine}")
            .Append($"Назва події: {userEvent.Title}{Environment.NewLine}")
            .Append($"Опис події: {userEvent.Description}{Environment.NewLine}")
            .Append($"Початок події: {userEvent.StartDate}{Environment.NewLine}")
            .Append($"Кінець події: {userEvent.EndDate}{Environment.NewLine}");

        await _eventReminderService.RemindAboutEventAsync(
            notification.TimeToRemind,
            sb.ToString(),
            notification.Event.UserId,
            notification.Event.Id);
    }
}