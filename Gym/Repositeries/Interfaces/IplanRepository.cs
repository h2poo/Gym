using Gym.Entities.Enums;

namespace Gym.Repositeries.Interfaces
{
    public interface IPlanRepository
    {
        Task<IReadOnlyList<Plan>> GetAllAsync();
        Task<Plan?> GetByIdAsync(int id);
        Task<bool> HasActiveOrUpcomingMembershipsAsync(int planId);
        Task UpdateAsync(Plan plan);
    }
}
