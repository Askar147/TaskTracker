using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData.Data;
using TaskTrackerData.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly TaskTrackerDataContext _context;

        public ProjectController(TaskTrackerDataContext context)
        {
            _context = context;
        }

        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _context.Projects.ToListAsync());
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                return Ok(project);
            }

            return NotFound();
        }

        // POST api/<ProjectController>/AddProject
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectRequest value)
        {
            var project = new Project()
            {
                Id = value.Id,
                Name = value.Name,
                Priority = value.Priority,
                ProjectStatus = value.ProjectStatus,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] ProjectRequest value)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                project.Name = value.Name;
                project.Priority = value.Priority;
                project.ProjectStatus = value.ProjectStatus;
                project.StartDate = value.StartDate;
                project.EndDate = value.EndDate;

                await _context.SaveChangesAsync();
                return Ok(project);
            }

            return NotFound();
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
