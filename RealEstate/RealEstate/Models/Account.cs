using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public char Status { get; set; }

        //public ICollection<User>? Users { get; set; }
    }
}
