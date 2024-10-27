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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoAddUser>>> GetUsers()
        {
            var users = await _context.Users
                .Select(user => new DtoAddUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImgPath = user.ImgPath
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DtoAddUser>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Votes)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new DtoAddUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ImgPath = user.ImgPath
            };

            return Ok(userDto);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<DtoAddUser>> CreateUser([FromBody] DtoAddUser newUserDto)
        {
            var newUser = new User
            {
                Name = newUserDto.Name,
                Email = newUserDto.Email,
                Password = newUserDto.Password, 
                ImgPath = newUserDto.ImgPath
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            newUserDto.Id = newUser.Id;

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUserDto);
        }

        // PUT: api/User/{id}
        [HttpPut("Update{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] DtoAddUser updatedUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Güncellenmiş bilgileri ayarla
            user.Name = updatedUserDto.Name;
            user.Email = updatedUserDto.Email;
            user.Password = updatedUserDto.Password; // Şifre hash işlemi eklenmeli
            user.ImgPath = updatedUserDto.ImgPath;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] DtoAddUser loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var userDto = new DtoAddUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ImgPath = user.ImgPath
            };

            return Ok(userDto);
        }
    }
}
