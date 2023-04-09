using Application.Common.Interfaces;
using Hangfire;
using Telegram.Bot;

namespace Infrastructure.Services;

public class EventReminderService : BaseMessageService, IEventReminderService
{
    public EventReminderService(ITelegramBotClient botClient) : base(botClient)
    {
    }

    public async Task CancelRemindingAboutEventAsync(Guid eventId)
    {
        await Task.Run(() => CancelRemindingAboutEvent(eventId));
    }

    public async Task RemindAboutEventAsync(DateTime time, string message, long receiverId, Guid eventId)
    {
        await Task.Run(() => RemindAboutEvent(time, message, receiverId, eventId));
    }

    public void RemindAboutEvent(DateTime remindAt, string message, long receiverId, Guid eventId)
    {

        var jobId = BackgroundJob.Schedule(() =>
                this.SendTextMessage(receiverId, message, CancellationToken.None),
            remindAt);
        JobStorage.Current.GetConnection().SetJobParameter(jobId, "eventId", eventId.ToString());
    }

    public void CancelRemindingAboutEvent(Guid eventId)
    {
        var jobStorage = JobStorage.Current;
        var scheduledCount = jobStorage.GetMonitoringApi().ScheduledCount();
        var scheduledJobs = jobStorage
            .GetMonitoringApi()
            .ScheduledJobs(0, (int)scheduledCount)
            .ToList();

        var connection = jobStorage.GetConnection();
        foreach (var scheduledJob in scheduledJobs)
        {
            var parseResult = Guid.TryParse(
                connection.GetJobParameter(scheduledJob.Key, "eventId"),
                out Guid id);

            if (parseResult && id == eventId)
            {
                BackgroundJob.Delete(scheduledJob.Key);
                break;
            }
        }
    }
}
