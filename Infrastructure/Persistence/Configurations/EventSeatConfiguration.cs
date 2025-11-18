using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventSeatConfiguration : IEntityTypeConfiguration<EventSeat>
{
    public void Configure(EntityTypeBuilder<EventSeat> builder)
    {
        builder.ToTable("EventSeat");

        builder.HasKey(e => e.EventSeatId);

        builder.Property(e => e.Row).IsRequired();
        builder.Property(e => e.Column).IsRequired();

        builder.Property(e => e.PosX).IsRequired();
        builder.Property(e => e.PosY).IsRequired();

        builder.Property(e => e.Price)
            .HasPrecision(10, 2)    
            .HasDefaultValue(0);

        builder.Property(e => e.Available)
            .HasDefaultValue(true);

        builder.HasOne(e => e.Event)
            .WithMany()
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EventSector)
            .WithMany(s => s.Seats)
            .HasForeignKey(e => e.EventSectorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.EventId, e.EventSectorId });
        builder.HasIndex(e => new { e.EventSectorId, e.Row, e.Column });
    }
}
