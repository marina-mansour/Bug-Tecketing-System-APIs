using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tecketing_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBugsController : ControllerBase
    {
        private readonly IUserBugManager _userBugManager;

        public UserBugsController(IUserBugManager userBugManager)
        {
            _userBugManager = userBugManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserBugReadDTO>>> GetAll()
        {
            var userBugs = await _userBugManager.GetAllAsync();
            if (userBugs is null)
            {
                return NotFound();
            }
            return Ok(userBugs);
        }
    }
}
