using FeedBackBoardApi.Data;
using FeedBackBoardApi.DTOs;
using FeedBackBoardApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedBackBoardApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedbackController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.Feedbacks.ToArray());
        }

        /// <summary>
        /// Feedback güncellemesi yapar.
        /// </summary>
        /// <param name="id">Feedback ID'si.</param>
        /// <returns>Güncellenen feedback</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DtoAddFeedBack model)
        {
            return Ok(id.ToString());
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoAddFeedBack model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var user = await _userManager.FindByIdAsync(userId);

            // feedback.UserId = userId;
            // feedback.Updated = DateTime.Now;

            var feedback = new Feedback
            {
                UserId = userId,
                Title = model.Title,
                Detail = model.Detail,
                Updated = DateTime.Now
            };
            // normalde Created bilgisini burada eklemek daha doğru çünkü kullanıcı eğer parametre olarak gönderirse
            // risk olur

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                feedback.Id
            });
        }
    }

    }
