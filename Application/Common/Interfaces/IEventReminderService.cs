namespace Application.Common.Interfaces;

public interface IEventReminderService
{
    Task RemindAboutEventAsync(
        DateTime remindAt, 
        string message, 
        long receiverId,
        Guid eventId);

    Task CancelRemindingAboutEventAsync(Guid eventId);
}
