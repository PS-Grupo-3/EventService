using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EventSectorConfiguration : IEntityTypeConfiguration<EventSector>
{
    public void Configure(EntityTypeBuilder<EventSector> builder)
    {
        builder.ToTable("EventSector");

        builder.HasKey(es => es.EventSectorId);

        builder.Property(es => es.EventSectorId)
            .ValueGeneratedNever();

        builder.Property(es => es.EventId)
            .IsRequired();

        builder.Property(es => es.SectorId)
            .IsRequired();

        builder.Property(es => es.Capacity)
            .IsRequired();

        builder.Property(es => es.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(es => es.Available)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne(es => es.Event)
            .WithMany(e => e.EventSectors)
            .HasForeignKey(es => es.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(es => es.EventId);
        builder.HasIndex(es => es.SectorId);
    }
}
