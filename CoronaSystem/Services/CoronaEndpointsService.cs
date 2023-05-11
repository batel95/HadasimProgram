using CoronaSystem.Data;
using CoronaSystem.Models;

namespace CoronaSystem.Services
{
	public static class CoronaEndpointsService
	{
		private static readonly UserDataService _userDataService = new UserDataService();

		public static IResult GetAllIlls ()
		{
			Func<User, bool> filter = u =>
			{
				if (u.Covid.Illness == null) return false;
				return u.Covid.Illness >= DateTime.Today;
			};
			IEnumerable<User>? users = GetByFilter (filter);
			ResponseUser[] toReturn = new [] { new ResponseUser () };
			if (users != null)
			{
				toReturn = ResponseUserService.ConvertUserListToResponseUserList (users.ToList ()).ToArray ();
			}
			return TypedResults.Ok (toReturn);
		}

		public static object GetAllVaccinated ()
		{
			Func<User, bool> filter = u => u.Covid.VaccinationDate1 != null;
			IEnumerable<User>? users = GetByFilter (filter);
			ResponseUser[] toReturn = new [] { new ResponseUser () };
			if (users != null)
			{
				toReturn = ResponseUserService.ConvertUserListToResponseUserList (users.ToList ()).ToArray ();
			}
			return TypedResults.Ok (toReturn);
		}

		private static IEnumerable<User>? GetByFilter (Func<User, bool> filter = null)
		{
			return _userDataService.GetByFilter (filter);
		}
	}
}
