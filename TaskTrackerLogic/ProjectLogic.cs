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

        public async Task<Project> CreateProject(Project project)
        {
            return await _repository.Create(project);
        }
    }
}
