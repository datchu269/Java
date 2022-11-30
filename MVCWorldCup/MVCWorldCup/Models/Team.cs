namespace MVCWorldCup.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int? TotalMatches { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public int Goal { get; set; }
        public int GoalsConceded { get; set; }
        public int? Difference { get; set; }
        public int? Point { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
