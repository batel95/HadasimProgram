using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoronaSystem.Data
{
	public static class DatabaseExtensions
	{
		public static void EnsureDatabaseCreated (this IHost host)
		{
			using var scope = host.Services.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<CoronaSystemDbContext>();

			context.Database.EnsureCreated ();
		}

	}
}
