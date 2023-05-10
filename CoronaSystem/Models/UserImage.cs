using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSystem.Models
{
	[Table ("Image")]
	public class UserImage
	{

		[DatabaseGenerated (DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Guid { get; set; }

		public bool Exist { get; set; }

		[Required]
		public DateTime LastUse { get; set; }

		[NotMapped]
		public String Uri { get; set; }

		public Guid UserGuid { get; set; }
		public virtual User User { get; set; }
	}
}