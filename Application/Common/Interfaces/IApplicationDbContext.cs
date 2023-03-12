namespace Application.Common.Interfaces;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventType> EventTypes { get; set; }

    // public DbSet<EventSchedule> EventSchedules { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}