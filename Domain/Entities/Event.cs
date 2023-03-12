using Domain.Common;

namespace Domain.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public long UserId { get; set; }

    public User User { get; set; } = default!;

    // public EventSchedule EventSchedule { get; set; } = default!;
}
