namespace Gym.Entities.Enums
{
    public class Trainer : GymUser
    {
       
        public DateTime HireDate { get; set; }

        public string Speciality { get; set; } = default!;

        public ICollection<Session> Sessions { get; set; }
            = new List<Session>();
    }
}
