using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCWorldCup.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Display(Name = "Team Name:")]
        public string TeamName { get; set; }

        [Display(Name = "Total Matches:")]
        public int? TotalMatches { get; set; }

        [Display(Name = "Win:")]
        public int Win { get; set; }

        [Display(Name = "Draw:")]
        public int Draw { get; set; }

        [Display(Name = "Lose:")]
        public int Lose { get; set; }

        [Display(Name = "Goal:")]
        public int Goal { get; set; }

        [Display(Name = "Goals Conceded:")]
        public int GoalsConceded { get; set; }

        [Display(Name = "Difference:")]
        public int? Difference { get; set; }

        [Display(Name = "Point:")]
        public int? Point { get; set; }

        [Display(Name = "GroupName:")]
        public string GroupName { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
