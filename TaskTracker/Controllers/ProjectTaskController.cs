using Microsoft.AspNetCore.Mvc;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskLogic _logicService;
        public ProjectTaskController(IProjectTaskLogic logicService) 
        {
            _logicService = logicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _logicService.GetAllProjectTasks());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            return Ok(await _logicService.GetSingleProjectTask(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logicService.CreateProjectTask(value));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logicService.UpdateProjectTask(id, value));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            return Ok(await _logicService.DeleteProjectTask(id));
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddTaskToProject(int projectId, [FromBody] ProjectTaskRequest value)
        {
            return Ok(await _logicService.AddTaskToProject(projectId, value));
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveTaskFromProject(int id)
        {
            return Ok(await _logicService.RemoveTaskFromProject(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            string? name, 
            string? description, 
            ProjectTaskStatus? taskStatus, 
            int? startPriority, 
            int? endPriority
            )
        {
            return Ok(await _logicService.SearchTask(name, description, taskStatus, startPriority, endPriority));
        }
    }
}
