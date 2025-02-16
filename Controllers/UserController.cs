using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListAPI.Models;

namespace TaskListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskListContext _context;

        public UserController(TaskListContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var foundUser = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (foundUser == null)
            {
                return NotFound();
            }

            return foundUser;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if(await _context.Users.AnyAsync(u => u.Username == user.Username)){
                return Conflict(new { message = "Username already exists, please try another."});
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> PostUserLogin(User user)
        {
            var foundUser = await _context.Users
                .Include(u => u.Tasks) // Carrega as Tasks relacionadas
                .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
            if (foundUser == null)
            {
                return NotFound(new { message = "Incorrect username or password." });
            }
            return foundUser;
        }
    }
}
