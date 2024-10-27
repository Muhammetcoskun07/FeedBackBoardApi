using System.ComponentModel.DataAnnotations;

namespace FeedBackBoardApi.Entities
{
	public enum Status
	{
		Planned,
		InProgress,
		Live
	}

	public class Post
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public int CommentId { get; set; }
		public int CategoryId { get; set; }

		public Status Status { get; set; }


		public Category Category { get; set; }
		public List<Comment> Comments { get; set; }
		public List<Vote> Votes { get; set; }

	}
}
