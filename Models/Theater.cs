using System.ComponentModel.DataAnnotations;

namespace NSC_project.Models
{
    public class Theater
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }
    }
}
