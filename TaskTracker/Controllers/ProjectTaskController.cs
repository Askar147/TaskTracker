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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _logicService.GetAllProjectTasks();

                if (!tasks.Any())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var task = await _logicService.GetSingleProjectTask(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTask([FromBody] ProjectTaskRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Project object is null");
                }

                return Ok(await _logicService.CreateProjectTask(value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTaskRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Task object is null");
                }

                return Ok(await _logicService.UpdateProjectTask(id, value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(int id)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTaskToProject(int projectId, [FromBody] ProjectTaskRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Task object is null");
                }

                return Ok(await _logicService.AddTaskToProject(projectId, value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
