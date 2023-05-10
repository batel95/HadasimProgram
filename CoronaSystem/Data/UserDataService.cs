using CoronaSystem.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoronaSystem.Data
{
	public class UserDataService
	{
		private static readonly DbContextOptions _options;
		public static async Task<User> Create (User entity)
		{
			using (var context = new CoronaSystemDbContext (_options))
			{
				EntityEntry<User> createdResult = await context.Set<User>().AddAsync(entity);
				await context.SaveChangesAsync ();

				return createdResult.Entity;
			}
		}

		public static async Task<User?> Get (string id)
		{
			using (var context = new CoronaSystemDbContext (_options))
			{
				return await context.Set<User> ().FirstOrDefaultAsync ((e) => e.IdNumber == id);
			}
		}

	}
}
