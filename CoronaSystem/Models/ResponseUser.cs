using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models {
	public class ResponseUser 
	{
		public String Name { get; set; }
		public String UserId { get; set; }
		public String Address { get; set; }
		public DateTime BirthDate { get; set; }
		public String PhoneNumber { get; set; }
		public String CellNumber { get; set; }
		public ResponseCovid CovidInfo { get; set; }

	}

	public record ResponseCovid(VaccinationResponse?[] Vaccinations, DateTime? Illness, DateTime? Recovery);

	public record VaccinationResponse(DateTime VaccinationDate, String VaccinationManufacturer);
}
