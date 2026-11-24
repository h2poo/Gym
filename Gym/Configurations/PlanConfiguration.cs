using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Configurations
{

    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.DurationDays)
                .IsRequired();

            builder.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_Plans_DurationDays",
                    "[DurationDays] >= 1 AND [DurationDays] <= 365");
            });

            builder.Property(x => x.Price)
                .HasPrecision(10, 2);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}
