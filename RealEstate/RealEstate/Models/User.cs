using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }


        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\\(?(\[0-9\]{3})\\)?\[-.●\]?(\[0-9\]{3})\[-.●\]?(\[0-9\]{4})$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public double Balance { get; set; }

        public string? Image { get; set; }

        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        //public ICollection<Transaction>? Transactions { get; set; }
        //public ICollection<TransactionExcept>? TransactionExcepts { get; set; }

    }
}
