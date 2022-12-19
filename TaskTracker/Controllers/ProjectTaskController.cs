using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
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
            try
            {
                return Ok(await _logicService.GetAllProjectTasks());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                return Ok(await _logicService.GetSingleProjectTask(id));
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ProjectTaskRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("project is null");
                }

                return Ok(await _logicService.CreateProjectTask(value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTaskRequest value)
        {
            try
            {
                return Ok(await _logicService.UpdateProjectTask(id, value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            try
            {
                return Ok(await _logicService.DeleteProjectTask(id));
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddTaskToProject(int projectId, [FromBody] ProjectTaskRequest value)
        {
            try
            {
                return Ok(await _logicService.AddTaskToProject(projectId, value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveTaskFromProject(int id)
        {
            try
            {
                return Ok(await _logicService.RemoveTaskFromProject(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
            try
            {
                return Ok(await _logicService.SearchTask(name, description, taskStatus, startPriority, endPriority));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
