using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models {
	public class RequestUser {
		[Required]
		public String FirstName { get; set; }
		[Required]
		public String LastName { get; set; }
		[Required]
		public String UserId { get; set; }
		[Required]
		public String City { get; set; }
		[Required]
		public String Street { get; set; }
		[Required]
		public int StreetNum { get; set; }
		[Required]
		public DateTime BirthDate { get; set; }
		[Required]
		public String PhoneNumber { get; set; }
		[Required]
		public String CellNumber { get; set; }
		public String? UserImage { get; set; }
		public DateTime? VaccinationDate1 { get; set; }
		public String? VaccinationManufacturer1 { get; set; }
		public DateTime? VaccinationDate2 { get; set; }
		public String? VaccinationManufacturer2 { get; set; }
		public DateTime? VaccinationDate3 { get; set; }
		public String? VaccinationManufacturer3 { get; set; }
		public DateTime? VaccinationDate4 { get; set; }
		public String? VaccinationManufacturer4 { get; set; }
		public DateTime? Illness { get; set; }
		public DateTime? Recovery { get; set; }
	}
}
