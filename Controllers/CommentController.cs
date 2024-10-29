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
    public class CommentController : ControllerBase
    {
       private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] DtoAddComment commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Comment data is null.");
            }

            var comment = new Comment
            {
                UserId = commentDto.UserId,
                Content = commentDto.CommentName,
                PostId = commentDto.PostId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok("Comment created successfully.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _context.Comments
                .Include(c => c.Post)
                .Select(c => new
                {
                    CommentId = c.Id,
                    Content = c.Content,
                    PostId = c.PostId,
                    PostTitle = c.Post.Title
                })
                .ToListAsync();

            return Ok(comments);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .Select(c => new
                {
                    CommentId = c.Id,
                    Content = c.Content,
                    PostId = c.PostId
                })
                .ToListAsync();

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            var response = new
            {
                CommentId = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId,
                PostTitle = comment.Post.Title
            };

            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] DtoAddComment commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Comment data is null.");
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            comment.Content = commentDto.CommentName;
            comment.PostId = commentDto.PostId;

            await _context.SaveChangesAsync();

            return Ok("Comment updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok("Comment deleted successfully.");
        }
    }
 }

