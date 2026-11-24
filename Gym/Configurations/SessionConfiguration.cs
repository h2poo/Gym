using Gym.Entities;
using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Configurations;


public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Capacity)
            .IsRequired();

        builder.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_Sessions_Capacity",
                "[Capacity] >= 1 AND [Capacity] <= 25");

            table.HasCheckConstraint(
                "CK_Sessions_Dates",
                "[EndDate] > [StartDate]");
        });

        builder.HasOne(x => x.Trainer)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.TrainerId)
            .IsRequired();
    }
}


