namespace Application.Common.Models;

public class DelayedMessageDto
{
    public string?  Id { get; set; }
    public DateTime EnqueueAt { get; set; }
    public string Text { get; set; } = string.Empty;

}