using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
    {
        public void Configure(EntityTypeBuilder<EventStatus> builder)
        {
            builder.ToTable("EventStatus");
            builder.HasKey(s => s.StatusId);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new EventStatus { StatusId = 1, Name = "Scheduled" },
                new EventStatus { StatusId = 2, Name = "Active" },
                new EventStatus { StatusId = 3, Name = "Postponed" },
                new EventStatus { StatusId = 4, Name = "Finished" }                
            );
        }
    }
}
