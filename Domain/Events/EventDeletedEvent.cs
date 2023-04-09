using Domain.Common;

namespace Domain.Events;

public class EventDeletedEvent:BaseEvent
{
    public Guid EventId { get; set; }

    public EventDeletedEvent(Guid eventId)
    {
        EventId = eventId;
    }
}
