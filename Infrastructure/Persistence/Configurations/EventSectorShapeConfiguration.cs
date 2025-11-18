using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventSectorShapeConfiguration : IEntityTypeConfiguration<EventSectorShape>
    {
        public void Configure(EntityTypeBuilder<EventSectorShape> builder)
        {
            builder.ToTable("EventSectorShape");

            builder.HasKey(s => s.EventSectorShapeId);

            builder.Property(s => s.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Colour)
                   .IsRequired()
                   .HasMaxLength(10)
                   .HasDefaultValue("#FFFFFF");

            builder.HasOne(shape => shape.EventSector) 
                   .WithOne(sector => sector.Shape)    
                   .HasForeignKey<EventSectorShape>(shape => shape.EventSectorId) 
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}