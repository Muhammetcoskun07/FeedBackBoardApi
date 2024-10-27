namespace FeedBackBoardApi.DTOs
{
    public class DtoAddVote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Count { get; set; }

    }
}
