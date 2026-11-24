namespace Gym.Entities.Enums
{

    public class Member :GymUser
    {
       
        public string? Photo { get; set; }

        public ICollection<MemberShip> MemberSubscriptions { get; set; } = default!;
       
    }
}
