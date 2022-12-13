﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private List<ProjectDTO> _projects = new List<ProjectDTO>
        {
            new ProjectDTO
            {
                Id = 0, Name = "First", Priority = 1, ProjectStatus = ProjectStatus.NotStarted, StartDate = new DateTime(2022, 01, 01), EndDate = new DateTime(2023, 01, 01)
            },
            new ProjectDTO
            {
                Id = 1, Name = "Second", Priority = 2, ProjectStatus = ProjectStatus.Completed, StartDate = new DateTime(2022, 02, 01), EndDate = new DateTime(2023, 01, 01)
            },
            new ProjectDTO
            {
                Id = 2, Name = "Third", Priority = 1, ProjectStatus = ProjectStatus.Active, StartDate = new DateTime(2022, 03, 01), EndDate = new DateTime(2023, 01, 01)
            }
        };

        // GET: api/<ProjectController>
        [HttpGet]
        public IEnumerable<ProjectDTO> Get()
        {
            return _projects;
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public ProjectDTO Get(int id)
        {
            return _projects.SingleOrDefault(x => x.Id == id);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public void Post([FromBody] ProjectDTO value)
        {
            _projects.Add(value);
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
