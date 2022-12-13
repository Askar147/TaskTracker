using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData;
using TaskTrackerData.Models;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly TaskTrackerDataContext _context;
        public ProjectTaskController(TaskTrackerDataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                return Ok(task);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ProjectTaskRequest value)
        {
            var project = await _context.Projects.FindAsync(value.ProjectId);

            if (project == null)
            {

                return BadRequest(new { message = $"No Project with such ProjectId: {value.ProjectId}" });
            }

            var task = new ProjectTask
            {
                Id = value.Id,
                ProjectId = value.ProjectId,
                Name = value.Name,
                Description = value.Description,
                Priority = value.Priority,
                TaskStatus = value.TaskStatus
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] ProjectTaskRequest value)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                task.ProjectId = value.ProjectId;
                task.Name = value.Name;
                task.Description = value.Description;
                task.Priority = value.Priority;
                task.TaskStatus = value.TaskStatus;

                await _context.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _context.Remove(task);
                await _context.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }
    }
}
