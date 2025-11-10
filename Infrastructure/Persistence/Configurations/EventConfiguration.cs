using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Event");
        builder.HasKey(e => e.EventId);

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.Address)
            .HasMaxLength(300);

        builder.Property(e => e.BannerImageUrl)
            .HasMaxLength(300);

        builder.Property(e => e.ThumbnailUrl)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(e => e.ThemeColor)
            .HasMaxLength(20)
            .HasDefaultValue("#FFFFFF");

        builder.Property(e => e.Created)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.Updated)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.Time)
            .IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CategoryType)
            .WithMany()
            .HasForeignKey(e => e.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.EventSectors)
            .WithOne(es => es.Event)
            .HasForeignKey(es => es.EventId);

        builder.HasIndex(e => e.VenueId);
        builder.HasIndex(e => e.CategoryId);
        builder.HasIndex(e => e.TypeId);         
        builder.HasIndex(e => e.StatusId);
        builder.HasIndex(e => new { e.CategoryId, e.TypeId });
    }
}
