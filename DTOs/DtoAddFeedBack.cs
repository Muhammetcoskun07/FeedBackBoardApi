namespace FeedBackBoardApi.DTOs
{
    public class DtoAddFeedBack
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
