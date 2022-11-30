namespace MVCWorldCup.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<Team> Team { get; set; }
    }
}
