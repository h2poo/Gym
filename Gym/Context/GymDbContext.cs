using Gym.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gym.Context
{
    public class GymDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GymDb;Trusted_Connection=True;TrustServerCertificate=true;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        DbSet<Member> Members { get; set; } 
        DbSet<Trainer> Trainers { get; set; }
        DbSet<Plan> Plans { get; set; }
        DbSet<MemberShip> MemberShips { get; set; }
        DbSet<Session> Sessions { get; set; }
    }
}
