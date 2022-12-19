using Microsoft.AspNetCore.Mvc;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectLogic _logic;

        public ProjectController(IProjectLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _logic.GetAllProjects());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProject(int id)
        {
            return Ok(await _logic.GetSingleProject(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectRequest value)
        {
            return Ok(await _logic.CreateProject(value));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectRequest value)
        {
            var project = await _logic.UpdateProject(id, value);

            return Ok(project);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            return Ok(await _logic.DeleteProject(id));
        }

        [HttpGet("from")]
        public async Task<IActionResult> GetAllTasksFromProject([FromQuery] int projectId)
        {
            var project = await _logic.GetAllProjects();

            if (project != null)
            {
                return Ok(project.Where(x => x.Id == projectId));
            }

            return NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? name, int? priority, ProjectTaskStatus? projectStatus)
        {
            return Ok(await _logic.SearchProject(name, priority, projectStatus));
        }
    }
}
