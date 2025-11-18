using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventSectorConfiguration : IEntityTypeConfiguration<EventSector>
{
    public void Configure(EntityTypeBuilder<EventSector> builder)
    {
        builder.ToTable("EventSector");

        builder.HasKey(e => e.EventSectorId);

        builder.Property(e => e.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.IsControlled)
            .IsRequired();

        builder.Property(e => e.Capacity)
            .IsRequired();

        builder.Property(e => e.Price)
            .HasPrecision(10, 2)
            .HasDefaultValue(0);

        builder.HasOne(e => e.Event)
            .WithMany(ev => ev.EventSectors)
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Shape)
            .WithOne(s => s.EventSector)
            .HasForeignKey<EventSectorShape>(s => s.EventSectorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.EventId);
        builder.HasIndex(e => e.VenueSectorId);
    }
}