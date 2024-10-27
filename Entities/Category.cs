namespace FeedBackBoardApi.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }

		public List<Post> Posts { get; set; }
	}
}
