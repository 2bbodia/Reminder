namespace Application.Common.Interfaces;

using Application.Common.Models;

public interface IRecurringMessageService
{
    Task<Guid> CreateMinutelyRecurringMessageAsync(string text, long receiverId);
    
    Task<Guid> CreateHourlyRecurringMessageAsync(string text, long receiverId, int? minute);
    
    Task<Guid> CreateDailyRecurringMessageAsync(string text, long receiverId,int? hour,int? minute);

    Task<Guid> CreateWeeklyRecurringMessageAsync(string text, long receiverId, DayOfWeek? dayOfWeek, int? hour, int? minute);

    Task<Guid> CreateMonthlyRecurringMessageAsync(string text, long receiverId, int? day, int? hour, int? minute);

    Task<Guid> CreateYearlyRecurringMessageAsync(string text, long receiverId, int? month, int? day, int? hour, int? minute);
    Task<IReadOnlyList<RecurringMessageDto>> GetAllByUserIdAsync(long id);

    Task CancelRecurringMessageAsync(string jobId);
}