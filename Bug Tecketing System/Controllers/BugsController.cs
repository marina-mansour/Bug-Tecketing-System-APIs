using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tecketing_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        private readonly IAttachmentManager _attachmentManager;
        private readonly IUserBugManager _userBugManager;

        public BugsController(IBugManager bugManager,IAttachmentManager attachmentManager,IUserBugManager userBugManager)
        {
            _bugManager = bugManager;
            _attachmentManager = attachmentManager;
            _userBugManager = userBugManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<BugReadDTO>>> GetAll()
        {
            var bug = await _bugManager.GetAllAsync();
            if (bug is null)
            {
                return NotFound();
            }
            return Ok(bug);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BugReadDTO>> GetByID(Guid id)
        {
            var bug = await _bugManager.GetBugByIDAsync(id);
            if (bug is null)
            {
                return NotFound();
            }
            return Ok(bug);
        }

        [HttpPost]
        public async Task<ActionResult> Add(BugAddDTO bug)
        {
            var res = await _bugManager.AddAsync(bug);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        /*--------------[AttachmentRoutes]--------------*/

        [HttpPost("{id}/attachments")]
        public async Task<ActionResult> AddAttachment([FromRoute] Guid id, [FromForm] AttachmentAddDTO att)
        {
            var res = await _attachmentManager.AddAsync(id,att);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("{id}/attachments")]
        public async Task<ActionResult> GetAttachment([FromRoute] Guid id)
        {
            var res = await _attachmentManager.GetAttachmentsByBugIdAsync(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        [HttpDelete("{id}/attachments/{attId}")]
        public async Task<ActionResult> DeleteAttachment([FromRoute] Guid id, [FromRoute] Guid attId)
        {
            var res = await _attachmentManager.DeleteAsync(id, attId);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


        /*--------------[Assignees(UserBugs)Routes]--------------*/

        [HttpPost("{id}/assignees")]
        public async Task<ActionResult> AddAssignees([FromRoute] Guid id, [FromBody] UserBugsAddDTO ubDTO)
        {
            var res = await _userBugManager.AddAsync(id,ubDTO);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpDelete("{id}/assignees/{userId}")]
        public async Task<ActionResult> DeleteAssignees([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            var res = await _userBugManager.DeleteAsync(userId,id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }


    }
}
