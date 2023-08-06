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
        sb.Append($"Нагадування про наступаючу подію:{Environment.NewLine}")
            .Append($"Назва події: {userEvent.Title}{Environment.NewLine}")
            .Append($"Опис події: {userEvent.Description}{Environment.NewLine}");

        await _eventReminderService.RemindAboutEventAsync(
            notification.TimeToRemind,
            sb.ToString(),
            notification.Event.UserId,
            notification.Event.Id);
    }
}