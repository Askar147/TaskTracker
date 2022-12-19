using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerLogic;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectLogic _logicService;

        public ProjectController(IProjectLogic logicService)
        {
            _logicService = logicService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _logicService.GetAllProjects();

                if (projects == null)
                {
                    return NotFound();
                }

                return Ok(projects);
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
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project = await _logicService.GetSingleProject(id);

                if (project == null)
                {
                    return NotFound();
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProject([FromBody] ProjectRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Project object is null");
                }

                return Ok(await _logicService.CreateProject(value));
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
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectRequest value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Project object is null");
                }

                return Ok(await _logicService.UpdateProject(id, value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            try
            {
                return Ok(await _logicService.DeleteProject(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("from")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasksFromProject([FromQuery] int projectId)
        {
            try
            {
                var projects = await _logicService.GetAllProjects();

                if (projects == null)
                {
                    return NotFound();
                    
                }
                return Ok(projects.Where(x => x.Id == projectId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(string? name, int? priority, ProjectStatus? projectStatus, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return Ok(await _logicService.SearchProject(name, priority, projectStatus, startDate, endDate));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
    }
}
