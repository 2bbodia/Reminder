using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class EventCreatedEvent : BaseEvent
{
    public EventCreatedEvent( Event ev, DateTime timeToRemind)
    {
        TimeToRemind = timeToRemind;
        Event = ev;
    }

    public Event Event { get; set; }

    public DateTime TimeToRemind { get; }

}
