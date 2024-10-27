using System.ComponentModel.DataAnnotations;

namespace FeedBackBoardApi.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentName { get; set; }
    }
}
