namespace FeedBackBoardApi.DTOs
{
    public class DtoAddComment
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string CommentName { get; set; }

    }
}
