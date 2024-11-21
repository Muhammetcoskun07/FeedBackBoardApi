using FeedBackBoardApi.Entities;

namespace FeedBackBoardApi.DTOs
{
    public class DtoAddPost
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int CategoryId { get; set; }
        public Status Status { get; set; }

    }
}
