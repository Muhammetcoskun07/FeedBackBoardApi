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
    public class VoteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllVotes()
        {
            var votes = await _context.Votes.ToListAsync();
            return Ok(votes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoteById(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound("Vote not found.");
            }

            return Ok(vote);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVote([FromBody] DtoAddVote voteDto)
        {
            if (voteDto == null)
            {
                return BadRequest("Vote data is null.");
            }

           
                var existingVote = await _context.Votes
                    .FirstOrDefaultAsync(v => v.UserId == voteDto.UserId && v.PostId == voteDto.PostId);

                if (existingVote != null)
                {
                    existingVote.Count += 1;
                }
                else
                {
                    var newVote = new Vote
                    {
                        UserId = voteDto.UserId,
                        ApplicationUser = await _context.Users.FindAsync(voteDto.UserId), 
                        PostId = voteDto.PostId,
                        Count = 1  
                    };

                    _context.Votes.Add(newVote);
                }

                await _context.SaveChangesAsync();

                return Ok("Vote updated successfully.");
           
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVote(int id, [FromBody] Vote updatedVote)
        {
            if (updatedVote == null || id != updatedVote.Id)
            {
                return BadRequest("Invalid vote data.");
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound("Vote not found.");
            }

            vote.UserId = updatedVote.UserId;
            vote.PostId = updatedVote.PostId;
            vote.Count = updatedVote.Count;

            await _context.SaveChangesAsync();

            return Ok("Vote updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound("Vote not found.");
            }

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();

            return Ok("Vote deleted successfully.");
        }
    }
}
