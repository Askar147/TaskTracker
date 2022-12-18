using Microsoft.AspNetCore.Mvc;
using TaskTracker.RequestModels;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskLogic _logic;
        public ProjectTaskController(IProjectTaskLogic logic) 
        {
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
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logic.UpdateProjectTask(id, value));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            return Ok(await _logic.DeleteProjectTask(id));
        }
    }
}
