using CoronaSystem.Models;

using Microsoft.EntityFrameworkCore;

namespace CoronaSystem.Data
{
	public class CoronaSystemDbContext : DbContext
	{
		public CoronaSystemDbContext (DbContextOptions options) : base (options)
		{
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Covid> Covids { get; set; }
		public virtual DbSet<UserImage> Images { get; set; }

		protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User> ()
		   .HasOne<Covid> (u => u.Covid)
		   .WithOne (c => c.User)
		   .HasForeignKey<Covid> (c => c.UserGuid);

			modelBuilder.Entity<User> ()
		   .HasOne<UserImage> (u => u.UserImage)
		   .WithOne (i => i.User)
		   .HasForeignKey<UserImage> (i => i.UserGuid);
		}

	}
}
