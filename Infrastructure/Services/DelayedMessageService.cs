namespace Infrastructure.Services;

using Application.Common.Interfaces;
using Telegram.Bot;
using AutoMapper;
using Hangfire;
using Application.Common.Models;
using Hangfire.Storage.Monitoring;


public class DelayedMessageService : BaseMessageService, IDelayedMessageService
{
    private readonly IMapper _mapper;
    public DelayedMessageService(ITelegramBotClient botClient, IMapper mapper) : base(botClient)
    {
        _mapper = mapper;
    }

    public async Task<string> CreateDelayedMessageAsync(string text, long receiverId, DateTime dateToSend)
    { 
        return await Task.Run(() => CreateDelayedMessage(text, receiverId,dateToSend));
    }
    
    public async Task<IReadOnlyList<DelayedMessageDto>> GetAllByUserIdAsync(long userId)
    {
        return await Task.Run(() => GetAllByUserId(userId));
    }
    
    public async Task<bool> CancelDelayedMessageAsync(string jobId)
    {
        return await Task.Run(() => CancelDelayedMessage(jobId));
    }
    

    private string CreateDelayedMessage(string text, long receiverId, DateTime dateToSend)
    {
        var jobId = BackgroundJob.Schedule(() => 
                this.SendTextMessage(receiverId, text, CancellationToken.None), 
            dateToSend);
        JobStorage.Current.GetConnection().SetJobParameter(jobId, "receiverId", receiverId.ToString());
        return jobId;

    }

    private IReadOnlyList<DelayedMessageDto> GetAllByUserId(long id)
    {
        var jobStorage = JobStorage.Current;
        var scheduledJobs = GetScheduledJobs();

        var connection = jobStorage.GetConnection();
        List<DelayedMessageDto> filteredMessages = new();
        foreach (var scheduledJob in scheduledJobs)
        {
            var parseResult = long.TryParse(
                connection.GetJobParameter(scheduledJob.Key, "receiverId"),
                out long receiverId);
            if (!parseResult) continue;

            if (receiverId == id)
            {
                var message = _mapper.Map<DelayedMessageDto>(scheduledJob);
                message.Text = (string)scheduledJob.Value.Job.Args[1];
                filteredMessages.Add(message);
            }
                
        }

        return filteredMessages;
    }

    private List<KeyValuePair<string, ScheduledJobDto>> GetScheduledJobs()
    {
        JobStorage jobStorage = JobStorage.Current;
        long scheduledCount = GetScheduledCount();
        var scheduledJobs = jobStorage
            .GetMonitoringApi()
            .ScheduledJobs(0, (int)scheduledCount)
            .ToList();
        return scheduledJobs;
    }

    private bool CancelDelayedMessage(string id)
    {
        return BackgroundJob.Delete(id);  
    }
    private long GetScheduledCount()
    {
        var jobStorage = JobStorage.Current;
        var scheduledCount = jobStorage.GetMonitoringApi().ScheduledCount();
        return scheduledCount;
    }
}