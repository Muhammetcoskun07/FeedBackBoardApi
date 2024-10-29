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

        // Tüm Oyları Getir
        [HttpGet("all")]
        public async Task<IActionResult> GetAllVotes()
        {
            var votes = await _context.Votes.ToListAsync();
            return Ok(votes);
        }

        // Belirli bir ID ile Oy Getir
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

            try
            {
                // Önce aynı kullanıcı ve post için var olan bir Vote kaydını arıyoruz
                var existingVote = await _context.Votes
                    .FirstOrDefaultAsync(v => v.UserId == voteDto.UserId && v.PostId == voteDto.PostId);

                if (existingVote != null)
                {
                    // Eğer kayıt varsa, Count'u artırıyoruz
                    existingVote.Count += 1;
                }
                else
                {
                    // Eğer kayıt yoksa, yeni bir Vote kaydı oluşturuyoruz
                    var newVote = new Vote
                    {
                        UserId = voteDto.UserId,
                        ApplicationUser = await _context.Users.FindAsync(voteDto.UserId), // Kullanıcıyı buluyoruz
                        PostId = voteDto.PostId,
                        Count = 1  // Yeni kayıt için Count başlangıç olarak 1
                    };

                    _context.Votes.Add(newVote);
                }

                // Değişiklikleri kaydediyoruz
                await _context.SaveChangesAsync();

                return Ok("Vote updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        // Oyu Güncelle
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

        // Oyu Sil
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
