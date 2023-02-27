namespace Application.Common.Models;

public class RecurringMessageDto
{
    public string Id { get; set; } = default!;

    public string Cron { get; set; } =default!;

    public string Queue { get; set; } = default!;

    public DateTime? NextExecution { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string TimeZoneId { get; set; } = default!;

    public string Text { get; set; } = string.Empty;

}
