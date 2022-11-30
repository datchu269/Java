using System.ComponentModel.DataAnnotations;

namespace WorldCup.Models
{
    public class WorldCup
    {
        public int Id { get; set; }
        public string Team { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public int Goal { get; set; }
        public int GoalsConceded { get; set; }
        public int Difference { get; set; }
        public int Point { get; set; }
    }
}
