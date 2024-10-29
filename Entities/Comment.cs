using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedBackBoardApi.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }   // Changed to string for compatibility
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string Content { get; set; }  // Renamed for clarity
    }
}
