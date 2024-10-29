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

		public ApplicationUser ApplicationUser { get; set; }                        
		public Post Post { get; set; }
	}
}
