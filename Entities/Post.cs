using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Status Status { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }
    }

}
