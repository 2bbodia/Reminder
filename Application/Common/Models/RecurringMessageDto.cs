namespace Application.Common.Models;

public class RecurringMessageDto
{
    public string Id { get; set; } = default!;

    public TimeSpan TimeSpan { get; set; } =default!;

    public DateTime? NextExecution { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Text { get; set; } = string.Empty;

}
