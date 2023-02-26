using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
	public class Product
	{
		public int Id { get; set; }

		[Required, StringLength(100)]
		public string NameProduct { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public string AddressProduct { get; set; }

		[Required]
		public char Directory { get; set; } //  cần bán - cho thuê

		[Required]
		public double Area { get; set; } // diện tích đất

		[Required]
		public int Bedroom { get; set; } // phòng ngủ

		[Required]
		public int Restrooms { get; set; } // nhà vs

		[Required]
		public string HomeOrientation { get; set; } // hướng nhà

		[Required]
		public string Description { get; set; }

		[Required]
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		//public ICollection<ProductDetail>? ProductDetails { get; set; }
		public ICollection<ProductImage>? ProductImages { get; set; }
		public ICollection<TransactionExcept>? TransactionExcepts { get; set; }
	}
}
