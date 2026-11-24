using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gym.Configurations
{
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnType("varchar(");
            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");
            builder.Property(x => x.Phone)
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");
        builder .HasIndex(x => x.Email)
                .IsUnique();
            builder.HasIndex(x => x.Phone)
                .IsUnique();
            builder.ComplexProperty(x=>x.Address);
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("GymUser_Email", "Email LIKE '%@%.%'");
                table.HasCheckConstraint("GymUser_Phone", "Len(Phone) = 11 AND (Phone Like '010' OR Phone Like '011' OR Phone Like '012' R Phone Like '015')");;
            });
        }
    }
}
