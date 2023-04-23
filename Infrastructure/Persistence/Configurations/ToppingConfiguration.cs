using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ToppingConfiguration : BaseEntityConfiguration<Topping>
{
    public override void Configure(EntityTypeBuilder<Topping> builder)
    {
        builder.Property(t => t.Name)
                .HasMaxLength(128)
                .IsRequired();

        base.Configure(builder);
    }
}
