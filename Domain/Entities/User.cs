using Domain.Common;

namespace Domain.Entities;

public class User
{
    // public Account Account { get; set; } = default!;
    public long Id { get; set; }
    public IEnumerable<Event> Events { get; set; } = default!;

}
