using System.ComponentModel.DataAnnotations;

namespace Gym.Entities.Enums
{
    public class Plan : BaseEntity

    {
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 365)]
        public int DurationDays { get; set; }

        [Range(0, 999999)]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MemberShip> PlanSubscriptions { get; set; } = default!;

    }
}
