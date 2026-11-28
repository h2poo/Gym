using Gym.Context;
using Gym.Entities.Enums;
using Gym.Repositeries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gym.Repositeries.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _context;

        public PlanRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Plan>> GetAllAsync()
        {
            return await _context.Plans
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Plan?> GetByIdAsync(int id)
        {
            return await _context.Plans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> HasActiveOrUpcomingMembershipsAsync(int planId)
        {
            var now = DateTime.Now;
            return await _context.MemberShips
                .AnyAsync(m => m.PlanId == planId && m.EndDate >= now);
        }

        public async Task UpdateAsync(Plan plan)
        {
            plan.UpdatedAt = DateTime.Now;
            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
        }
    }
}
