using Microsoft.EntityFrameworkCore;

namespace HRM.Model
{
	[PrimaryKey("Id")]
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public double Salary { get; set; }
	}
}
