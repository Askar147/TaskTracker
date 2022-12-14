using Microsoft.EntityFrameworkCore;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Repositories;

namespace TaskTrackerLogic
{
    public class ProjectLogic
    {
        private readonly IRepository<Project> _repository;
        public ProjectLogic(IRepository<Project> repository) 
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _repository.GetAll();
        }

        public async Task<Project> GetSingleProject(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Project> CreateProject(ProjectRequest value)
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

            return await _repository.Create(project);
        }

        public async Task<Project> UpdateProject(ProjectRequest value)
        {
            var project = await _repository.GetById(value.Id);
            if (project == null)
            {
                throw new ArgumentException(nameof(value));
            }

            project.Name = value.Name;
            project.Priority = value.Priority;
            project.ProjectStatus = value.ProjectStatus;
            project.StartDate = value.StartDate;
            project.EndDate = value.EndDate;

            _repository.Update(project);

            return project;
        }
    }
}
