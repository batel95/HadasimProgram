using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models {
	public class Covid {

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid Guid { get; set; }

		[Column("VaccinationDate1", TypeName = "DateTime2")]
		public DateTime? VaccinationDate1 { get; set; }

		[Column("VaccinationManufacturer1", TypeName = "ntext")]
		[MaxLength(50)]
		public String? VaccinationManufacturer1 { get; set; }

		[Column("VaccinationDate2", TypeName = "DateTime2")]
		public DateTime? VaccinationDate2 { get; set; }

		[Column("VaccinationManufacturer2", TypeName = "ntext")]
		[MaxLength(50)]
		public String? VaccinationManufacturer2 { get; set; }

		[Column("VaccinationDate3", TypeName = "DateTime2")]
		public DateTime? VaccinationDate3 { get; set; }

		[Column("VaccinationManufacturer3", TypeName = "ntext")]
		[MaxLength(50)]
		public String? VaccinationManufacturer3 { get; set; }

		[Column("VaccinationDate4", TypeName = "DateTime2")]
		public DateTime? VaccinationDate4 { get; set; }

		[Column("VaccinationManufacturer4", TypeName = "ntext")]
		[MaxLength(50)]
		public String? VaccinationManufacturer4 { get; set; }


		[Column("IllnessDate", TypeName = "DateTime2")]
		public DateTime? Illness { get; set; }

		[Column("RecoveryDate", TypeName = "DateTime2")]
		public DateTime? Recovery { get; set; }

		[ForeignKey("UserGuid")]
		public Guid UserGuid { get; set; }
		public virtual User User { get; set; }
	}
}