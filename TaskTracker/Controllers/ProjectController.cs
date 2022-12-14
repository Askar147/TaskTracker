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
        private readonly TaskTrackerDataContext _context;
        private readonly ProjectLogic _logic;

        public ProjectController(TaskTrackerDataContext context, IRepository<Project> repository)
        {
            _context = context;
            _logic = new ProjectLogic(repository);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _logic.GetAllProjects());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _logic.GetSingleProject(id);

            if (project != null)
            {
                return Ok(project);
            }

            return NotFound();
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
            var project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                _context.Remove(project);
                await _context.SaveChangesAsync();
                return Ok(project);
            }

            return NotFound();
        }
    }
}
