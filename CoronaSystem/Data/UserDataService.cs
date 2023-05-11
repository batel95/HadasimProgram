using CoronaSystem.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoronaSystem.Data
{
	public class UserDataService
	{
		private readonly DbContextOptions<CoronaSystemDbContext> _contextOptions;
		private Predicate<User> NoFilter = x => true;

		public UserDataService()
        {
			_contextOptions = new DbContextOptionsBuilder<CoronaSystemDbContext> ()
			.UseSqlServer (System.Configuration.ConfigurationManager.ConnectionStrings["CoronaSystemContext"].ConnectionString)
			.Options;
		}
        public async Task<User> Create (User entity)
		{
			using (var context = new CoronaSystemDbContext(_contextOptions))
			{
				EntityEntry<User> createdResult = await context.Set<User>().AddAsync(entity);
				await context.SaveChangesAsync ();

				return createdResult.Entity;
			}
		}

		public async Task<User?> Get (string id)
		{
			using (var context = new CoronaSystemDbContext (_contextOptions))
			{
				User? entity = await context.Users
					.Include(u => u.UserImage)
					.Include(u => u.Covid)
					.FirstOrDefaultAsync((u) => u.IdNumber == id);
				return entity;
			}
		}

		public IEnumerable<User> GetByFilter (Func<User, bool> filter = null)
		{
			if (filter == null)
			{
				filter = u => true;
			}
			using (var context = new CoronaSystemDbContext (_contextOptions))
			{
				IEnumerable<User> entities = context.Users
					.Include(u => u.UserImage)
					.Include(u => u.Covid)
					.Where(filter)
					.ToList();

				return entities;
			}
		}

	}
}
