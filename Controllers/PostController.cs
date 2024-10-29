using FeedBackBoardApi.Data;
using FeedBackBoardApi.DTOs;
using FeedBackBoardApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedBackBoardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _uploadsFolder = Path.Combine("wwwroot", "images");

        public PostController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] DtoAddPost postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var post = new Post
                {
                    Title = postDto.Title,
                    Detail = postDto.Detail,
                    CategoryId = postDto.CategoryId,
                    Status = postDto.Status // Eğer durumu da gönderiyorsanız
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return Ok("Post created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        // Tüm Postları Getirme
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Votes)
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .Select(p => new
                {
                    PostId = p.Id,
                    Title = p.Title,
                    Detail = p.Detail,
                    VoteCount = p.Votes.Sum(v => v.Count),
                    CommentCount = p.Comments.Count,
                    Status = p.Status,
                    Category = p.Category != null ? p.Category.CategoryName : "No Category"
                })
                .ToListAsync();

            return Ok(posts);
        }

        // ID ile Post Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Votes)
                .Include(p => p.Comments)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var response = new
            {
                PostId = post.Id,
                Title = post.Title,
                Detail = post.Detail,
                VoteCount = post.Votes.Sum(v => v.Count),
                CommentCount = post.Comments.Count,
                Status = post.Status,
                Category = post.Category != null ? post.Category.CategoryName : "No Category"
            };

            return Ok(response);
        }


        [HttpGet("uploads")]
        public IActionResult GetUploads()
        {
            if (!Directory.Exists(_uploadsFolder))
            {
                return Ok(new string[] { });
            }

            var files = Directory.GetFiles(_uploadsFolder).Select(Path.GetFileName).ToArray();
            return Ok(files);
        }

        // Yüklenen Resmi Silme (Delete Upload)
        [HttpDelete("FotoDelete/{fileName}")]
        public IActionResult DeleteUpload(string fileName)
        {
            var filePath = Path.Combine(_uploadsFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            System.IO.File.Delete(filePath);
            return Ok("File deleted successfully.");
        }
    }
}
