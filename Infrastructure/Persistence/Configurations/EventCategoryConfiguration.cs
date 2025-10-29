using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.ToTable("EventCategory");
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.CategoryTypes)
                .WithOne(ct => ct.EventCategory)
                .HasForeignKey(ct => ct.EventCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Events)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new EventCategory { CategoryId = 1, Name = "Music" },
                new EventCategory { CategoryId = 2, Name = "Stand-up" },
                new EventCategory { CategoryId = 3, Name = "Conference" }
            );
        }
    }
}
