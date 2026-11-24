using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.JoinDate)
                .HasDefaultValueSql("GETDATE()");

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.BuildingNumber)
                    .HasColumnName("BuildingNumber")
                    .HasMaxLength(20);

                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(100);

                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasMaxLength(50);
            });
        }
    }
}
    