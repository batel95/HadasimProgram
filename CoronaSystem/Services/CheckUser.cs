using CoronaSystem.Models;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CoronaSystem.Services {
	public class CheckUser {
		public async Task<IActionResult> (RequestUser rUser) {
			User user = new();
			String notes = ""; //Notes to send if the user added and there are problems.
			user.UserId = new Guid();
			if (LegalName(rUser.FirstName) && LegalName(rUser.LastName)) {
				user.FullName = rUser.FirstName + " " + rUser.LastName;
			}
			else 
			{
				return TypedResults.BadRequest("Illegal name");
			}

			if (LegalId(rUser.UserId)) 
			{
				user.IdNumber = rUser.UserId;
			}
			else {
				return TypedResults.BadRequest("Illegal Id number");
			}
			if (await LegalAddress(rUser.City, rUser.Street)) 
			{
				user.City = rUser.City;
				user.Address = rUser.Street + " " + rUser.StreetNum;
			}
			else {
				return TypedResults.BadRequest("Illegal address");
			}
			if (rUser.BirthDate <= DateTime.Today && rUser.BirthDate >= new DateTime(1900,1,1)) {
				user.BirthDate = rUser.BirthDate;
			}
			else {
				return TypedResults.BadRequest("Future or before 1900 year date of birth");
			}
			if (await LegalPhone(rUser.PhoneNumber)) {
				user.PhoneNumber = rUser.PhoneNumber;
			}
			else {
				return TypedResults.BadRequest("wrong phone number");
			}
			if (await LegalPhone(rUser.CellNumber)) {
				user.CellNumber = rUser.CellNumber;
			}
			else {
				return TypedResults.BadRequest("wrong cellphone number");
			}
			if (rUser.UserImage == null && rUser.UserImage == "") {
				user.UserImage = null;
			}
			else if (!LegalImage(rUser.UserImage)) {
				notes += "Wrong image file! Not added\n";
				user.UserImage = null;
			}
			else {
				user.UserImage = CheckImage.AddUserImage(rUser.UserImage, user);
			}
			user.Covid = new Covid();
			if (rUser.VaccinationDate1 != null) {
				if (rUser.VaccinationManufacturer1 != null) {
					if (rUser.VaccinationDate1 > user.BirthDate && rUser.VaccinationDate1 <= DateTime.Today) {
						user.Covid.VaccinationDate1 = rUser.VaccinationDate1;
						user.Covid.VaccinationManufacturer1 = rUser.VaccinationManufacturer1;
						if (rUser.VaccinationDate2 != null) {
							if (rUser.VaccinationManufacturer2 != null) {
								if (rUser.VaccinationDate2 > rUser.VaccinationDate1 && rUser.VaccinationDate2 <= DateTime.Today) {
									user.Covid.VaccinationDate2 = rUser.VaccinationDate2;
									user.Covid.VaccinationManufacturer2 = rUser.VaccinationManufacturer2;
									if (rUser.VaccinationDate3 != null) {
										if (rUser.VaccinationManufacturer3 != null) {
											if (rUser.VaccinationDate3 > rUser.VaccinationDate2 && rUser.VaccinationDate3 <= DateTime.Today) {
												user.Covid.VaccinationDate3 = rUser.VaccinationDate3;
												user.Covid.VaccinationManufacturer3 = rUser.VaccinationManufacturer3;
												if (rUser.VaccinationDate4 != null) {
													if (rUser.VaccinationManufacturer4 != null) {
														if (rUser.VaccinationDate4 > rUser.VaccinationDate3 && rUser.VaccinationDate4 <= DateTime.Today) {
															user.Covid.VaccinationDate4 = rUser.VaccinationDate4;
															user.Covid.VaccinationManufacturer4 = rUser.VaccinationManufacturer4;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			if (rUser.Illness != null && rUser.Illness >= user.BirthDate && rUser.Illness <= DateTime.Today) {
				user.Covid.Illness = rUser.Illness;
				if (rUser.Recovery != null &&  rUser.Recovery > user.Covid.Illness) {
					user.Covid.Recovery = rUser.Recovery;
				}
				else {
					user.Covid.Recovery = user.Covid.Illness.Value.AddDays(7);
				}
			}






		}

		private bool LegalId(string userId) {
			int digit, step, ctr = 0;
			if (userId.Length > 9)
				return false;
			if (! userId.All(c => (c >= '0' && c <= '9')))
				return false;
			userId = "000000000" + userId;
			userId = userId.Substring(userId.Length - 9);
			for (int i = 0; i < userId.Length; i++) {
				digit = (int)userId[i];
				step = digit * ((i % 2) + 1);
				ctr += step > 9 ? step - 9 : step;
			}
			return ctr % 10 == 0;			
		}

		private bool LegalName(String name) //Legal name is Hebrew 
		{
			return name.All(c => (c >= 'א' && c <= 'ת' || c == ' '));
		}

		private async Task<bool> LegalAddress(String city, String street) {
			if (!(LegalName(city) && LegalName(street)))
				return false;
			 //Check if the city and street is exist in israel
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(@"https://data.gov.il/api/3/action/datastore_search?resource_id=bf185c7f-1a4e-4662-88c5-fa118a244bda&filters={" + String.Format("\"city_name\":\"{0} \",\"street_name\":\"{1} \"}", city, street));
			var response = await client.GetStringAsync(client.BaseAddress);
			Res r = JsonSerializer.Deserialize<Res>(response);
			return (r.result.total > 0);
		}

		private async Task<bool> LegalPhone(String phone) //Legal phone number
{
			if (phone[0] != '0')
				return false;

			if (phone[1] == '2' || phone[1] == '3' || phone[1] == '4' || phone[1] == '8' || phone[1] == '9') {
				if (phone.Length == 9) {
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else if (phone.Length == 10) {
					if (phone[2] != '-')
						return false;
					phone = phone.Substring(0, 2) + phone.Substring(3, 7);
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else return false;

				phone = phone.Substring(0, 2) + "-" + phone.Substring(2, 7);
			}
			else if (phone[1] == '7') {
				if (phone.Length == 10) {
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else if (phone.Length == 11) {
					if (phone[3] != '-')
						return false;
					phone = phone.Substring(0, 3) + phone.Substring(3, 7);
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else return false;

				phone = phone.Substring(0, 3) + "-" + phone.Substring(3, 7);

			}
			else
				return false;

			return await CanExistPhone(phone);
		}


		private async Task<bool> LegalCellphone(String phone) //Legal phone number
{
			if (phone[0] != '0' || phone[1] != 5)
				return false;

			if (phone[2] == '0' || phone[2] == '1' || phone[2] == '2' || phone[2] == '3' || phone[2] == '4' || phone[2] == '5' || phone[2] == '8') {
				if (phone.Length == 10) {
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else if (phone.Length == 11) {
					if (phone[3] != '-')
						return false;
					phone = phone.Substring(0, 3) + phone.Substring(3, 7);
					if (!phone.All(c => c >= '0' && c <= '9')) {
						return false;
					}
				}
				else return false;

				phone = phone.Substring(0, 3) + "-" + phone.Substring(3, 7);
			}
			else
				return false;

			return await CanExistPhone(phone);
		}

		private async Task<bool> CanExistPhone(String phone) {

		}

		private bool LegalImage(String image)
{
		}
	}

	public class Res {
		public Result result { get; set; }
	}
	public class Result {
		public int total { get; set; }
	}
}
