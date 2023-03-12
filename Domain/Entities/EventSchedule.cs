
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class EventSchedule : BaseEntity
{
    public int Frequency { get; set; }

    public DateTime LastRun { get; set; }

    public DateTime NextRun { get; set; }
    public Periodicity Periodicity { get; set; }
    //public Guid EventId { get; set; }

   // public Event Event { get; set; } = default!;

}
