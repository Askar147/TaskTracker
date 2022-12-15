using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData.Data;
using TaskTrackerData.Entities;
using TaskTrackerData.Repositories;
using TaskTrackerLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> UpdateProject([FromBody] ProjectRequest value)
        {
            var project = await _logic.UpdateProject(value);

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
    }
}
