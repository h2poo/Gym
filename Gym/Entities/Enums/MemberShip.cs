using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Entities.Enums
{
    public class MemberShip :BaseEntity

    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  int MemberId { get; set; }
        public Member Member { get; set; } = default!;
        public  int PlanId { get; set; }
        public Plan Plan { get; set; } = default!;
        [NotMapped]
        public string  Status => DateTime.Now < StartDate ? "Upcoming" :
        DateTime.Now > EndDate ?  "Active":
        "Expired";

    }
}
