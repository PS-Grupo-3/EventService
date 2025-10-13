using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Address)
                .HasMaxLength(300);

            builder.Property(e => e.MapUrl)
                .HasMaxLength(300);

            builder.Property(e => e.BannerImageUrl)
                .HasMaxLength(300);

            builder.Property(e => e.ThumbnailUrl)
                .HasMaxLength(300);

            builder.Property(e => e.ThemeColor)
                .HasMaxLength(50);

            builder.HasOne(e => e.EventStatus)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.Status)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.EventCategory)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
