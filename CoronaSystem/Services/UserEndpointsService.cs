﻿using CoronaSystem.Data;
using CoronaSystem.Models;

using Microsoft.EntityFrameworkCore;

namespace CoronaSystem.Services
{
	public static class UserEndpointsService
	{
		private static readonly UserDataService _userDataService = new UserDataService();

		public static IResult GetAll ()
		{
			IEnumerable<User>? users = GetByFilter ();
			ResponseUser[] toReturn = new [] { new ResponseUser () };
			if (users != null)
			{
				toReturn = ResponseUserService.ConvertUserListToResponseUserList (users.ToList ()).ToArray ();
			}
			return TypedResults.Ok (toReturn);
		}

		public static IResult GetByCity (string city)
		{
			Func<User, bool> filter = u => u.City == city;
			IEnumerable<User>? users = GetByFilter (filter);
			ResponseUser[] toReturn = new [] { new ResponseUser () };
			if (users != null)
			{
				toReturn = ResponseUserService.ConvertUserListToResponseUserList (users.ToList ()).ToArray ();
			}
			return TypedResults.Ok (toReturn);
		}

		public static async Task<IResult> GetById (int id)
		{
			String idStr = id.ToString ();
			idStr = new String ('0', 9 - idStr.Length) + idStr;
			User? user = await _userDataService.Get (idStr);
			ResponseUser toReturn = new();
			if (user != null)
			{
				ResponseUserService.ConvertUserToResponseUser (user, toReturn);
			}
			return TypedResults.Ok (toReturn);
		}

		private static IEnumerable<User>? GetByFilter (Func<User, bool> filter = null)
		{
			return _userDataService.GetByFilter (filter);
		}
	}
}
