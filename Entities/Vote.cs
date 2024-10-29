namespace FeedBackBoardApi.Entities
{
	public class Vote
	{
		public int Id { get; set; }            
		public int UserId { get; set; }          
		public int PostId { get; set; }          
		public int Count { get; set; }           

		
		//public User User { get; set; }          
		public Post Post { get; set; }           
	}

}
