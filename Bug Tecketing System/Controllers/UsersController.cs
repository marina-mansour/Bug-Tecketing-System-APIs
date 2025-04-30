using System.Security.Claims;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tecketing_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;

        public UsersController(IUserManager userManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAll()
        {
            var users = await _userManager.GetAllAsync();
            if (users is null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            var res = await _userManager.AddAsync(user);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var token = await _authManager.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }

        [HttpPut("for-manager/{id}")]
        [Authorize(Policy = Constants.Policies.ForManagerOnly)]
        public async Task<IActionResult> ManagerUpdate(Guid id, [FromBody] UserManagerUpdateDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var res = await _userManager.ManagerUpdateAsync(user);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UserUpdate(Guid id, [FromBody] UserUpdateDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }

            var userClaims = HttpContext.User;

            var userIdFromToken = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleFromToken = userClaims.FindFirst(ClaimTypes.Role)?.Value;

            Console.WriteLine($"User ID from Token: {userIdFromToken}");
            Console.WriteLine($"Role from Token: {roleFromToken}");

            if (userIdFromToken != user.Id.ToString())
                return Unauthorized("Token does not match the user being updated.");

            var res = await _userManager.UpdateAsync(user);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("for-manager")]
        [Authorize(Policy = Constants.Policies.ForManagerOnly)]
        public async Task<ActionResult<IEnumerable<string>>> GetForManager()
        {
            var result = await _userManager.GetManagerDataAsync(User);
            return Ok(result);
        }

        [HttpGet("for-testing")]
        [Authorize(Policy = Constants.Policies.ForTestingOnly)]
        public async Task<ActionResult<IEnumerable<string>>> GetForTesting()
        {
            var result = await _userManager.GetTestingDataAsync(User);
            return Ok(result);
        }

        [HttpGet("for-developing")]
        [Authorize(Policy = Constants.Policies.ForDevelopingOnly)]
        public async Task<ActionResult<IEnumerable<string>>> GetForDeveloping()
        {
            var result = await _userManager.GetDevelopingDataAsync(User);
            return Ok(result);
        }

    }
}
