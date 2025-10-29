using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CategoryTypeConfiguration : IEntityTypeConfiguration<CategoryType>
{
    public void Configure(EntityTypeBuilder<CategoryType> builder)
    {
        builder.ToTable("CategoryType");

        builder.HasKey(ct => ct.TypeId);

        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(ct => ct.EventCategory)
            .WithMany(ec => ec.CategoryTypes)
            .HasForeignKey(ct => ct.EventCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new CategoryType { TypeId = 1, EventCategoryId = 1, Name = "Rock" },
            new CategoryType { TypeId = 2, EventCategoryId = 1, Name = "Pop" },
            new CategoryType { TypeId = 3, EventCategoryId = 1, Name = "Trap" },
            new CategoryType { TypeId = 4, EventCategoryId = 1, Name = "Reggaeton" },
            new CategoryType { TypeId = 5, EventCategoryId = 1, Name = "Electronic" },
            new CategoryType { TypeId = 6, EventCategoryId = 1, Name = "Metal" },
            new CategoryType { TypeId = 7, EventCategoryId = 1, Name = "Cumbia" },
            new CategoryType { TypeId = 8, EventCategoryId = 1, Name = "Hip-Hop" },
            new CategoryType { TypeId = 9, EventCategoryId = 2, Name = "Comedy" },
            new CategoryType { TypeId = 10, EventCategoryId = 2, Name = "Satire" },
            new CategoryType { TypeId = 11, EventCategoryId = 3, Name = "Technology" },
            new CategoryType { TypeId = 12, EventCategoryId = 3, Name = "Business" },
            new CategoryType { TypeId = 13, EventCategoryId = 3, Name = "Education" }
        );
    }
}
