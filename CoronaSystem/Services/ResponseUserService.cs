using CoronaSystem.Models;

namespace CoronaSystem.Services
{
	public static class ResponseUserService
	{
		public static Action<User, ResponseUser> ConvertUserToResponseUser = (user, responseUser) =>
		{
			VaccinationResponse? [] vaccinationResponses;
			responseUser.Name = user.FullName;
			responseUser.UserId = user.IdNumber;
			responseUser.Address = user.Address + user.City;
			responseUser.BirthDate = user.BirthDate;
			responseUser.PhoneNumber = user.PhoneNumber;
			responseUser.CellNumber = user.CellNumber;
			responseUser.HesImage = user.UserImage != null && user.UserImage.Exist;

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

			responseUser.CovidInfo = new ResponseCovid(vaccinationResponses, user.Covid.Illness, user.Covid.Recovery);
		};

		public static IEnumerable<ResponseUser> ConvertUserListToResponseUserList (List<User> users)
		{
			List<ResponseUser> responseUsers = new(users.Capacity);
			Parallel.ForEach (users, user => 
			{ ConvertUserToResponseUser (user, responseUsers [users.FindIndex (u => u.UserId == user.UserId)]); });
			return responseUsers;
		}
	}
}
