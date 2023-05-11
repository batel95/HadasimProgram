using CoronaSystem.Models;

namespace CoronaSystem.Services
{
	public static class ResponseUserService
	{
		public static ResponseUser ConvertUserToResponseUser (User user)
		{
			ResponseUser toReturn = new ResponseUser ();
			VaccinationResponse? [] vaccinationResponses;
			toReturn.Name = user.FullName;
			toReturn.UserId = user.IdNumber;
			toReturn.Address = user.Address + user.City;
			toReturn.BirthDate = user.BirthDate;
			toReturn.PhoneNumber = user.PhoneNumber;
			toReturn.CellNumber = user.CellNumber;
			toReturn.HesImage = user.UserImage != null && user.UserImage.Exist;

            if (user.Covid.VaccinationDate4 != null)
            {
				vaccinationResponses = new VaccinationResponse [4];
				vaccinationResponses[3] = new VaccinationResponse(user.Covid.VaccinationDate4.Value, user.Covid.VaccinationManufacturer4);
				vaccinationResponses [2] = new VaccinationResponse (user.Covid.VaccinationDate3.Value, user.Covid.VaccinationManufacturer3);
				vaccinationResponses [1] = new VaccinationResponse (user.Covid.VaccinationDate2.Value, user.Covid.VaccinationManufacturer2);
				vaccinationResponses [0] = new VaccinationResponse (user.Covid.VaccinationDate1.Value, user.Covid.VaccinationManufacturer1);
			}
			else if (user.Covid.VaccinationDate3 != null)
            {
				vaccinationResponses = new VaccinationResponse [3];
				vaccinationResponses[2] = new VaccinationResponse(user.Covid.VaccinationDate3.Value, user.Covid.VaccinationManufacturer3);
				vaccinationResponses [1] = new VaccinationResponse (user.Covid.VaccinationDate2.Value, user.Covid.VaccinationManufacturer2);
				vaccinationResponses [0] = new VaccinationResponse (user.Covid.VaccinationDate1.Value, user.Covid.VaccinationManufacturer1);
			}
			else if (user.Covid.VaccinationDate2 != null)
			{
				vaccinationResponses = new VaccinationResponse [2];
				vaccinationResponses [1] = new VaccinationResponse (user.Covid.VaccinationDate2.Value, user.Covid.VaccinationManufacturer2);
				vaccinationResponses [0] = new VaccinationResponse (user.Covid.VaccinationDate1.Value, user.Covid.VaccinationManufacturer1);
			}
			else if (user.Covid.VaccinationDate1 != null)
			{
				vaccinationResponses = new VaccinationResponse [1];
				vaccinationResponses [0] = new VaccinationResponse (user.Covid.VaccinationDate1.Value, user.Covid.VaccinationManufacturer1);
			}
			else
			{
				vaccinationResponses = new VaccinationResponse [0];
			}

			toReturn.CovidInfo = new ResponseCovid(vaccinationResponses, user.Covid.Illness, user.Covid.Recovery);

			return toReturn;

		}
	}
}
