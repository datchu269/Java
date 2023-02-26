using System.ComponentModel.DataAnnotations;

namespace NSC_project.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Director { get; set; }

        [Required, StringLength(100)]
        public string Actor { get; set; }

        [Required]
        public int Duration_min { get; set; }

        [Display(Name = "Opening Date"), DataType(DataType.Date)]
        public DateTime Opening_date { get; set; }

        [Required, StringLength(100)]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
