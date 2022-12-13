using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData;
using TaskTrackerData.Models;
using TaskTrackerData.Statuses;

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
            _context= context;
        }

        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _context.Projects.ToListAsync());
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return _context.Projects.SingleOrDefault(x => x.Id == id);
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

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var act = _projects.Find(x => x.Id == id);
            if (act != null)
            {
                _projects.Remove(act);
            }
        }
    }
}
