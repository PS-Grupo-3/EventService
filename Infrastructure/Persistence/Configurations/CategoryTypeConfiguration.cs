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
            new CategoryType { TypeId = 13, EventCategoryId = 3, Name = "Education" },
            
            new CategoryType { TypeId = 14, EventCategoryId = 4, Name = "Football" },
            new CategoryType { TypeId = 15, EventCategoryId = 4, Name = "Basketball" },
            new CategoryType { TypeId = 16, EventCategoryId = 4, Name = "Tennis" },
            new CategoryType { TypeId = 17, EventCategoryId = 4, Name = "Rugby" },
            new CategoryType { TypeId = 18, EventCategoryId = 4, Name = "MMA" },
            new CategoryType { TypeId = 19, EventCategoryId = 4, Name = "Boxing" },
            new CategoryType { TypeId = 20, EventCategoryId = 4, Name = "Hockey" },
            new CategoryType { TypeId = 21, EventCategoryId = 4, Name = "Motorsport" },
            new CategoryType { TypeId = 22, EventCategoryId = 4, Name = "Volleyball" },
            new CategoryType { TypeId = 23, EventCategoryId = 4, Name = "Handball" },
            new CategoryType { TypeId = 24, EventCategoryId = 4, Name = "Paddle" },
            new CategoryType { TypeId = 25, EventCategoryId = 4, Name = "Swimming" },
            new CategoryType { TypeId = 26, EventCategoryId = 4, Name = "Athletics" },
            
            new CategoryType { TypeId = 27, EventCategoryId = 5, Name = "Musical" },
            new CategoryType { TypeId = 28, EventCategoryId = 5, Name = "Drama" },
            new CategoryType { TypeId = 29, EventCategoryId = 5, Name = "Classic Theatre" },
            new CategoryType { TypeId = 30, EventCategoryId = 5, Name = "Children Theatre" },
            new CategoryType { TypeId = 31, EventCategoryId = 5, Name = "Modern Theatre" },
            new CategoryType { TypeId = 32, EventCategoryId = 5, Name = "Dance Theatre" },
            
            new CategoryType { TypeId = 33, EventCategoryId = 6, Name = "Ballet" },
            new CategoryType { TypeId = 34, EventCategoryId = 6, Name = "Contemporary Dance" },
            new CategoryType { TypeId = 35, EventCategoryId = 6, Name = "Opera" },
            new CategoryType { TypeId = 36, EventCategoryId = 6, Name = "Classical Music" },
            new CategoryType { TypeId = 37, EventCategoryId = 6, Name = "Exhibitions" },
            
            new CategoryType { TypeId = 38, EventCategoryId = 7, Name = "Tech" },
            new CategoryType { TypeId = 39, EventCategoryId = 7, Name = "Cooking" },
            new CategoryType { TypeId = 40, EventCategoryId = 7, Name = "Fitness" },
            new CategoryType { TypeId = 41, EventCategoryId = 7, Name = "Art Workshop" }
            
        );
    }
}
