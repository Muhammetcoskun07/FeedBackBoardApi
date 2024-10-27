using System.ComponentModel.DataAnnotations;

namespace FeedBackBoardApi.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
		public int PostId { get; set; }
		public string CommentName { get; set; }

		public User User { get; set; }                        
		public Post Post { get; set; }
	}
}
