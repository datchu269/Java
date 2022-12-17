using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MCVManagerEmployees.Models
{
    public class Employee
    {
        [Required]
        [Display(Name = "EmployeeID")]
        [RegularExpression(@"[A-Z]{2,50}$+[0-9]{3,50}$", ErrorMessage = "Only uppercase Characters are allowed.")]
        [StringLength(50, MinimumLength = 5)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"[A-Z]{2,50}$", ErrorMessage = "Only uppercase Characters are allowed.")]
        public string Department { get; set; }

        [Required]
        public double Salary { get; set; }
    }
}
