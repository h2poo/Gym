using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gym.Context
{
    public class GymDbContext : DbContext
    {
        public GymDbContext()
        {
        }

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GymDb;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        DbSet<Member> Members { get; set; }
        DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        DbSet<Session> Sessions { get; set; }


    }
}
