using FeedBackBoardApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedBackBoardApi.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Vote> Votes { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
				.HasOne<User>()
				.WithMany()
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Post>()
				.HasOne<Category>()
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Comment>()
				.HasOne<Post>()
				.WithMany()
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Vote>()
				.HasOne(v => v.User)
				.WithMany()
				.HasForeignKey(v => v.UserId)
				.OnDelete(DeleteBehavior.NoAction); // Değişiklik

			modelBuilder.Entity<Vote>()
				.HasOne(v => v.Post)
				.WithMany()
				.HasForeignKey(v => v.PostId)
				.OnDelete(DeleteBehavior.NoAction); // Değişiklik

			modelBuilder.Entity<User>().Ignore(u => u.Img);

			base.OnModelCreating(modelBuilder);
		}
	}
}
