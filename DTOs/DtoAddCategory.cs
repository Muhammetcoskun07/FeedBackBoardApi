using FeedBackBoardApi.Entities;

namespace FeedBackBoardApi.DTOs
{
    public class DtoAddCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public Status Status { get; set; }
    }
}
