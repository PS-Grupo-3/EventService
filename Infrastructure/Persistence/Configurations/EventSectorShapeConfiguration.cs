using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventSectorShapeConfiguration : IEntityTypeConfiguration<EventSectorShape>
{
    public void Configure(EntityTypeBuilder<EventSectorShape> builder)
    {
        builder.ToTable("EventSectorShape");

        builder.HasKey(e => e.EventSectorShapeId);

        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Width).IsRequired();
        builder.Property(e => e.Height).IsRequired();
        builder.Property(e => e.X).IsRequired();
        builder.Property(e => e.Y).IsRequired();
        builder.Property(e => e.Rotation).IsRequired();
        builder.Property(e => e.Padding).IsRequired();
        builder.Property(e => e.Opacity).IsRequired();
        builder.Property(e => e.Colour)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.EventSector)
            .WithOne(s => s.Shape)
            .HasForeignKey<EventSectorShape>(s => s.EventSectorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.EventSectorId)
            .IsUnique();
    }
}