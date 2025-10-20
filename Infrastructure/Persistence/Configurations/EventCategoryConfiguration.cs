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

            builder.HasData(
                new EventCategory { CategoryId = 1, Name = "Rock" },
                new EventCategory { CategoryId = 2, Name = "Pop" },
                new EventCategory { CategoryId = 3, Name = "Trap" },
                new EventCategory { CategoryId = 4, Name = "Reggaeton" },
                new EventCategory { CategoryId = 5, Name = "Electronic" },
                new EventCategory { CategoryId = 6, Name = "Metal" },
                new EventCategory { CategoryId = 7, Name = "Cumbia" },
                new EventCategory { CategoryId = 8, Name = "Hip-Hop" }
            );
        }
    }
}
