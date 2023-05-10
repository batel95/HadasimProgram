using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSystem.Models
{
	[Table ("UserInfo")]
	public class User
	{
		[DatabaseGenerated (DatabaseGeneratedOption.Identity)]
		[Column ("Identification")]
		[Key]
		public Guid UserId { get; set; }

		[Column ("Name", TypeName = "ntext")]
		[MaxLength (50)]
		[Required]
		public String FullName { get; set; }

		[Column ("Id", TypeName = "ntext")]
		[MaxLength (9)]
		[Required]
		public String IdNumber { get; set; }

		[Column ("Address", TypeName = "ntext")]
		[Required]
		public String Address { get; set; }

		[Column ("City", TypeName = "ntext")]
		[Required]
		public String City { get; set; }

		[Column ("BirthDate", TypeName = "DateTime2")]
		[Required]
		public DateTime BirthDate { get; set; }

		[Column ("Phone", TypeName = "ntext")]
		[MaxLength (10)]
		[Required]
		public String PhoneNumber { get; set; }

		[Column ("Cellphone", TypeName = "ntext")]
		[MaxLength (10)]
		[Required]
		public String CellNumber { get; set; }


		public virtual UserImage? UserImage { get; set; }

		public virtual Covid Covid { get; set; }



	}
}
