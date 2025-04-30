using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bug_Tecketing_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManager _prjManager;

        public ProjectsController(IProjectManager prjManager)
        {
            _prjManager = prjManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectReadDTO>>> GetAll()
        {
            var prjs = await _prjManager.GetAllAsync();
            if (prjs is null)
            {
                return NotFound();
            }
            return Ok(prjs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDTO>> GetByID(Guid id)
        {
            var prj = await _prjManager.GetProjectByIDAsync(id);
            if (prj is null)
            {
                return NotFound();
            }
            return Ok(prj);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProjectAddDTO prj)
        {
            var res = await _prjManager.AddAsync(prj);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
