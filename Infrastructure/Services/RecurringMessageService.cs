namespace Infrastructure.Services;

using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Cronos;
using Hangfire;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using Telegram.Bot;


public class RecurringMessageService : BaseMessageService, IRecurringMessageService
{
    private readonly IMapper _mapper;

    public RecurringMessageService(ITelegramBotClient botClient, IMapper mapper) : base(botClient)
    {
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RecurringMessageDto>> GetAllByUserIdAsync(long id)
    {
        return await Task.Run(() => GetAllByUserId(id));
    }

    public async Task<Guid> CreateMinutelyRecurringMessageAsync(string text, long receiverId)
    {
        return await Task.Run(() => this.CreateMinutelyRecurringMessage(text, receiverId));
    }


    public async Task<Guid> CreateHourlyRecurringMessageAsync(string text, long receiverId, int? minute)
    {
        return await Task.Run(() => this.CreateHourlyRecurringMessage(text, receiverId,minute));
    }


    public async Task<Guid> CreateDailyRecurringMessageAsync(string text, long receiverId, int? hour, int? minute)
    {
        return await Task.Run(() => this.CreateDailyRecurringMessage(text, receiverId, hour,minute));
    }


    public async Task<Guid> CreateWeeklyRecurringMessageAsync(string text, long receiverId, DayOfWeek? dayOfWeek, int? hour, int? minute)
    {
        return await Task.Run(() => this.CreateWeeklyRecurringMessage(text, receiverId, dayOfWeek,hour,minute));
    }


    public async Task<Guid> CreateMonthlyRecurringMessageAsync(string text, long receiverId, int? day, int? hour, int? minute)
    {
        return await Task.Run(() => this.CreateMonthlyRecurringMessage(text, receiverId,day,hour,minute));
    }


    public async Task<Guid> CreateYearlyRecurringMessageAsync(string text, long receiverId, int? month, int? day, int? hour, int? minute)
    {
        return await Task.Run(() => this.CreateYearlyRecurringMessage(text, receiverId, month,day,hour,minute));
    }


    public async Task CancelRecurringMessageAsync(string jobId)
    {
        await Task.Run(() => this.CancelRecurringMessage(jobId));
    }


    public Guid CreateMinutelyRecurringMessage(string text, long receiverId)
    {
        return CreateRecurringMessage(text, receiverId, Cron.Minutely());
    }

    public Guid CreateHourlyRecurringMessage(string text, long receiverId, int? minute)
    {
        string time = minute.HasValue ? Cron.Hourly(minute.Value) : Cron.Hourly();
        return CreateRecurringMessage(text, receiverId, time);
    }

    public Guid CreateDailyRecurringMessage(string text, long receiverId, int? hour, int? minute)
    {
        string time = hour.HasValue && minute.HasValue ? Cron.Daily(hour.Value, minute.Value) : Cron.Daily();

        return CreateRecurringMessage(text, receiverId, time);
    }

    public Guid CreateWeeklyRecurringMessage(string text, long receiverId, DayOfWeek? dayOfWeek, int? hour, int? minute)
    {
        string time = dayOfWeek.HasValue && hour.HasValue && minute.HasValue ? Cron.Weekly(dayOfWeek.Value,hour.Value, minute.Value) : Cron.Weekly();

        return CreateRecurringMessage(text, receiverId, time);
    }
    public Guid CreateMonthlyRecurringMessage(string text, long receiverId, int? day, int? hour, int? minute)
    {
        string time = day.HasValue && hour.HasValue && minute.HasValue ? Cron.Monthly(day.Value, hour.Value, minute.Value) : Cron.Monthly();

        return CreateRecurringMessage(text, receiverId, time);
    }

    public Guid CreateYearlyRecurringMessage(string text, long receiverId, int? month, int? day, int? hour, int? minute)
    {
        string time = month.HasValue && day.HasValue && hour.HasValue && minute.HasValue ? Cron.Yearly(month.Value,day.Value, hour.Value, minute.Value) : Cron.Yearly();

        return CreateRecurringMessage(text, receiverId, time);
    }
    public IReadOnlyList<RecurringMessageDto> GetAllByUserId(long id)
    {
        var jobStorage = JobStorage.Current;
        var connection = jobStorage.GetConnection();
        var recurringJobs = connection.GetRecurringJobs();

        List<RecurringMessageDto> filteredMessages = new();
        foreach (var recurringJob in recurringJobs)
        {
            CronExpression cron = CronExpression.Parse(recurringJob.Cron);
            var nextOccurenceFromNow = cron.GetNextOccurrence(DateTime.UtcNow);
            var nextOccurenceFromPrevious = cron.GetNextOccurrence(nextOccurenceFromNow!.Value);

            var timeSpan = nextOccurenceFromPrevious!.Value - nextOccurenceFromNow!.Value;            

            long.TryParse(recurringJob.Id.Split("_")[0],out long receiverId);
            if (receiverId == id)
            {
                var message = _mapper.Map<RecurringMessageDto>(recurringJob);
                message.Text = (string)recurringJob.Job.Args[1];
                message.TimeSpan = timeSpan;
                filteredMessages.Add(message);
            }

        }

        return filteredMessages;
    }
    
    public Guid CreateRecurringMessage(string text, long receiverId, string time)
    {
        Guid jobId = Guid.NewGuid();
        string id = receiverId + "_" + jobId.ToString();
        RecurringJob.AddOrUpdate(
            id,
            () => this.SendTextMessage(receiverId, text, CancellationToken.None),
           time);
        return jobId;
    }

    public void CancelRecurringMessage(string jobId)
    {
        RecurringJob.RemoveIfExists(jobId);
    }


}