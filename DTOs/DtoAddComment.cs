namespace FeedBackBoardApi.DTOs
{
    public class DtoAddComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string CommentName { get; set; }

    }
}
