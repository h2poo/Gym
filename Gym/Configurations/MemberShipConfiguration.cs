using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Member)
                .WithMany(x => x.MemberSubscriptions)
                .HasForeignKey(x => x.MemberId)
                .IsRequired();

            builder.HasOne(x => x.Plan)
                .WithMany(x => x.PlanSubscriptions)
                .HasForeignKey(x => x.PlanId)
                .IsRequired();
        }
    }
}
