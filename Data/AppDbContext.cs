using FeedBackBoardApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedBackBoardApi.Data
{
	public class AppDbContext:DbContext
	{

		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
				.HasOne<User>()
				.WithMany()
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Cascade);  

			modelBuilder.Entity<Post>()
				.HasOne<Category>()
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);  

			modelBuilder.Entity<Comment>()
				.HasOne<Post>()
				.WithMany()
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.Cascade);  

			base.OnModelCreating(modelBuilder);
		}

	}
}