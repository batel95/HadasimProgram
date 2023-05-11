using CoronaSystem.Data;
using CoronaSystem.Models;

namespace CoronaSystem.Services
{
	public static class UserService
	{
		private static readonly UserDataService _userDataService = new UserDataService();

		public static async Task<IResult> InsertUser (RequestUser rUser)
		{
			User user = new();
			User? tempUser;
			user.UserId = new Guid ();
			if (LegalName (rUser.FirstName) && LegalName (rUser.LastName))
			{
				user.FullName = rUser.FirstName + " " + rUser.LastName;
			}
			else
			{
				return TypedResults.BadRequest ("Illegal name");
			}

			if (LegalId (rUser.UserId))
			{
				user.IdNumber = rUser.UserId;
			}
			else
			{
				return TypedResults.BadRequest ("Illegal Id number");
			}
			if (LegalAddress (rUser.City, rUser.Street))
			{
				user.City = rUser.City;
				user.Address = rUser.Street + " " + rUser.StreetNum;
			}
			else
			{
				return TypedResults.BadRequest ("Illegal address");
			}
			if (rUser.BirthDate <= DateTime.Today && rUser.BirthDate >= new DateTime (1900, 1, 1))
			{
				user.BirthDate = rUser.BirthDate;
			}
			else
			{
				return TypedResults.BadRequest ("Future or before 1900 year date of birth");
			}
			if (LegalPhone (rUser.PhoneNumber))
			{
				user.PhoneNumber = rUser.PhoneNumber;
			}
			else
			{
				return TypedResults.BadRequest ("wrong phone number");
			}
			if (LegalCellphone (rUser.CellNumber))
			{
				user.CellNumber = rUser.CellNumber;
			}
			else
			{
				return TypedResults.BadRequest ("wrong cellphone number");
			}
			if (rUser.UserImage == null)
			{
				user.UserImage = null;
			}
			else if (!LegalImage (rUser.UserImage).Result)
			{
				rUser.UserImage = null;
				user.UserImage = null;
			}
			else
			{
				user.UserImage = ImageService.CreateUserImage (rUser.UserImage, user).Result;
			}
			user.Covid = new Covid ();
			if (rUser.VaccinationDate1 != null && rUser.VaccinationManufacturer1 != null && rUser.VaccinationDate1 > user.BirthDate && rUser.VaccinationDate1 <= DateTime.Today)
			{
				user.Covid.VaccinationDate1 = rUser.VaccinationDate1;
				user.Covid.VaccinationManufacturer1 = rUser.VaccinationManufacturer1;
				if (rUser.VaccinationDate2 != null && rUser.VaccinationManufacturer2 != null && rUser.VaccinationDate2 > rUser.VaccinationDate1 && rUser.VaccinationDate2 <= DateTime.Today)
				{
					user.Covid.VaccinationDate2 = rUser.VaccinationDate2;
					user.Covid.VaccinationManufacturer2 = rUser.VaccinationManufacturer2;
					if (rUser.VaccinationDate3 != null && rUser.VaccinationManufacturer3 != null && rUser.VaccinationDate3 > rUser.VaccinationDate2 && rUser.VaccinationDate3 <= DateTime.Today)
					{
						user.Covid.VaccinationDate3 = rUser.VaccinationDate3;
						user.Covid.VaccinationManufacturer3 = rUser.VaccinationManufacturer3;
						if (rUser.VaccinationDate4 != null && rUser.VaccinationManufacturer4 != null && rUser.VaccinationDate4 > rUser.VaccinationDate3 && rUser.VaccinationDate4 <= DateTime.Today)
						{
							user.Covid.VaccinationDate4 = rUser.VaccinationDate4;
							user.Covid.VaccinationManufacturer4 = rUser.VaccinationManufacturer4;
						}
						else
						{
							rUser.VaccinationDate4 = null;
							rUser.VaccinationManufacturer4 = "";
						}
					}
					else
					{
						rUser.VaccinationDate3 = null;
						rUser.VaccinationManufacturer3 = "";
						rUser.VaccinationDate4 = null;
						rUser.VaccinationManufacturer4 = "";
					}
				}
				else
				{
					rUser.VaccinationDate2 = null;
					rUser.VaccinationManufacturer2 = "";
					rUser.VaccinationDate3 = null;
					rUser.VaccinationManufacturer3 = "";
					rUser.VaccinationDate4 = null;
					rUser.VaccinationManufacturer4 = "";
				}
			}
			else
			{
				rUser.VaccinationDate1 = null;
				rUser.VaccinationManufacturer1 = "";
				rUser.VaccinationDate2 = null;
				rUser.VaccinationManufacturer2 = "";
				rUser.VaccinationDate3 = null;
				rUser.VaccinationManufacturer3 = "";
				rUser.VaccinationDate4 = null;
				rUser.VaccinationManufacturer4 = "";
			}

			if (rUser.Illness != null && rUser.Illness >= user.BirthDate && rUser.Illness <= DateTime.Today)
			{
				user.Covid.Illness = rUser.Illness;
				if (rUser.Recovery != null && rUser.Recovery > user.Covid.Illness)
				{
					user.Covid.Recovery = rUser.Recovery;
				}
				else
				{
					user.Covid.Recovery = user.Covid.Illness.Value.AddDays (7);
				}
			}

			tempUser = await _userDataService.Get (user.IdNumber);
			if (tempUser != null)
			{
				return TypedResults.BadRequest ("This person is in the system");
			}

			tempUser = await _userDataService.Create (user);

			if (tempUser == null)
			{
				return TypedResults.UnprocessableEntity ();
			}

			return TypedResults.Created<RequestUser> ($"/UserToControllers/{rUser.UserId}", rUser);

		}

		private static bool LegalId (string userId)
		{
			int digit, step, ctr = 0;
			if (userId.Length > 9)
				return false;
			if (!userId.All (c => (c >= '0' && c <= '9')))
				return false;
			userId = "000000000" + userId;
			userId = userId.Substring (userId.Length - 9);
			for (int i = 0; i < userId.Length; i++)
			{
				digit = (int) userId [i];
				step = digit * ((i % 2) + 1);
				ctr += step > 9 ? step - 9 : step;
			}
			return ctr % 10 == 0;
		}

		private static bool LegalName (String name) //Legal name is Hebrew 
		{
			return name.All (c => (c >= 'א' && c <= 'ת' || c == ' '));
		}

		private static bool LegalAddress (String city, String street)
		{
			if (!(LegalName (city) && LegalName (street)))
				return false;
			//Check if the city and street is exist in israel
			return HttpClientService.ExistInTheGovernmentDatabase (EGovernmentDatabaseTypes.Addresses, city, street).Result;
		}

		private static bool LegalPhone (String phone) //Legal phone number
		{
			if (phone [0] != '0')
				return false;

			if (phone [1] == '2' || phone [1] == '3' || phone [1] == '4' || phone [1] == '8' || phone [1] == '9')
			{
				if (phone.Length == 9)
				{
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else if (phone.Length == 10)
				{
					if (phone [2] != '-')
						return false;
					phone = phone.Substring (0, 2) + phone.Substring (3, 7);
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else
					return false;

				phone = phone.Substring (0, 2) + "-" + phone.Substring (2, 7);
			}
			else if (phone [1] == '7')
			{
				if (phone.Length == 10)
				{
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else if (phone.Length == 11)
				{
					if (phone [3] != '-')
						return false;
					phone = phone.Substring (0, 3) + phone.Substring (3, 7);
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else
					return false;

				phone = phone.Substring (0, 3) + "-" + phone.Substring (3, 7);

			}
			else
				return false;

			return CanExistPhone (phone);
		}

		private static bool LegalCellphone (String phone) //Legal phone number
		{
			if (phone [0] != '0' || phone [1] != 5)
				return false;

			if (phone [2] == '0' || phone [2] == '1' || phone [2] == '2' || phone [2] == '3' || phone [2] == '4' || phone [2] == '5' || phone [2] == '8')
			{
				if (phone.Length == 10)
				{
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else if (phone.Length == 11)
				{
					if (phone [3] != '-')
						return false;
					phone = phone.Substring (0, 3) + phone.Substring (3, 7);
					if (!phone.All (c => c >= '0' && c <= '9'))
					{
						return false;
					}
				}
				else
					return false;

				phone = phone.Substring (0, 3) + "-" + phone.Substring (3, 7);
			}
			else
				return false;

			return CanExistPhone (phone);
		}

		private static bool CanExistPhone (String phone)
		{
			String prePaidPhone = "055-91";
			if (phone.StartsWith (prePaidPhone))
				return true;

			return HttpClientService.ExistInTheGovernmentDatabase (EGovernmentDatabaseTypes.Phones, AddEndX (4, phone)).Result
				|| HttpClientService.ExistInTheGovernmentDatabase (EGovernmentDatabaseTypes.Phones, AddEndX (5, phone)).Result
				|| HttpClientService.ExistInTheGovernmentDatabase (EGovernmentDatabaseTypes.Phones, AddEndX (6, phone)).Result;
		}

		private static Func<int, String, String> AddEndX = (num, str)
			=> str.Substring (0, num + 1) + new String('X', str.Length - (num + 1));

		private static async Task<bool> LegalImage (IFormFile image)
		{
			if (image.Length <= 0)
				return false;
			string path = @"./Temp"; // or whatever 
			var ext = Path.GetExtension (image.FileName).ToLowerInvariant ();
			bool toReturn = false;
			if (!Directory.Exists (path))
			{
				DirectoryInfo di = Directory.CreateDirectory(path);
			}
			using (var stream = System.IO.File.Create (Path.Combine (path, image.FileName)))
			{
				await image.CopyToAsync (stream);
				toReturn = ImageService.IsLegalImage (stream, ext);
			}
			if (File.Exists (Path.Combine (path, image.FileName)))
				File.Delete (Path.Combine (path, image.FileName));
			return toReturn;
		}
	}
}

