namespace FeedBackBoardApi.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public IFormFile? Img { get; set; }
		public string? ImgPath { get; set; }

		public List<Comment> Comments { get; set; }            
		public List<Vote> Votes { get; set; }
	}
}
