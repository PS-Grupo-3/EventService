using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
    {
        public void Configure(EntityTypeBuilder<EventStatus> builder)
        {
            builder.HasKey(s => s.StatusId);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new EventStatus { StatusId = 1, Name = "Borrador" },
                new EventStatus { StatusId = 2, Name = "Publicado" },
                new EventStatus { StatusId = 3, Name = "Pospuesto" },
                new EventStatus { StatusId = 4, Name = "Cancelado" },
                new EventStatus { StatusId = 5, Name = "Finalizado" }
            );
        }
    }
}
