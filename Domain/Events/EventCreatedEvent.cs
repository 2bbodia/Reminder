using Domain.Common;

namespace Domain.Events;

public class EventCreatedEvent : BaseEvent
{
    public EventCreatedEvent(long receiverId, string message, DateTime timeToRemind)
    {
        ReceiverId = receiverId;
        Message = message;
        TimeToRemind = timeToRemind;
    }

    public long ReceiverId {get;}
    public string Message { get; }

    public DateTime TimeToRemind { get; }
}
