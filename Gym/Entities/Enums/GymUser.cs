namespace Gym.Entities.Enums
{
    public class GymUser : BaseEntity

    {
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = default!;

        public Address Address { get; set; } = new();

        public DateTime JoinDate { get; set; }

    }
}
