using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData.Data;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly TaskTrackerDataContext _context;
        private readonly IProjectTaskLogic _logic;
        public ProjectTaskController(TaskTrackerDataContext context, IProjectTaskLogic logic) 
        {
            _context = context;
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _logic.GetAllProjectTasks());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            return Ok(await _logic.GetSingleProjectTask(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logic.CreateProjectTask(value));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logic.UpdateProjectTask(value));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            return Ok(await _logic.DeleteProjectTask(id));
        }

        [HttpGet("from")]
        public async Task<IActionResult> GetAllTasksFromProject([FromQuery] int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Id == projectId)
                .SingleOrDefaultAsync();

            if (project != null)
            {
                return Ok(project.Tasks);
            }

            return NotFound();
        }
    }
}
