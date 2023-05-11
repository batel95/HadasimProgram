using CoronaSystem.Data;
using CoronaSystem.Models;

using Microsoft.EntityFrameworkCore;

namespace CoronaSystem.Services
{
	public static class UserEndpointsService
	{
		private static readonly UserDataService _userDataService = new UserDataService();

		public static async Task<IResult> GetAll ()
		{
			IEnumerable<User>? users = await _userDataService.GetAll ();
			ResponseUser[] toReturn = new [] { new ResponseUser () };
			if (users != null)
			{
				toReturn = ResponseUserService.ConvertUserListToResponseUserList (users.ToList ()).ToArray ();
			}
			return TypedResults.Ok (toReturn);
		}
	}
}
