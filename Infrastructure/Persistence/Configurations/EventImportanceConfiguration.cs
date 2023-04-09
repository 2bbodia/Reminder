using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventImportanceConfiguration : IEntityTypeConfiguration<EventImportance>
{
    public void Configure(EntityTypeBuilder<EventImportance> builder)
    {
        builder.HasData(new[]
        {
            new EventImportance { Id = (int)Domain.Enums.EventImportance.Low, Name = "Low" },
            new EventImportance { Id = (int)Domain.Enums.EventImportance.Medium, Name = "Medium" },
            new EventImportance { Id = (int)Domain.Enums.EventImportance.High, Name = "High" },
        });

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Name).HasMaxLength(30).IsRequired();
    }
}
