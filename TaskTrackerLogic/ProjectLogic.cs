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
    }
}
