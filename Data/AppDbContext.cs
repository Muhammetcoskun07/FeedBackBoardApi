using FeedBackBoardApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedBackBoardApi.Data
{
	public class AppDbContext:DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

	}
}