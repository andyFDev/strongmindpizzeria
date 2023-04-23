using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PizzaConfiguration : BaseEntityConfiguration<Pizza>
{
    public override void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.Property(t => t.Name)
                .HasMaxLength(128)
                .IsRequired();

        base.Configure(builder);
    }
}
