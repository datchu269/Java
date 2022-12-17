using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCWorldCup.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Display(Name = "Group Name:")]
        public string GroupName { get; set; }

        public ICollection<Team> Team { get; set; }
    }
}
