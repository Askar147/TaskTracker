using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerData.Repositories;

namespace TaskTrackerLogic
{
    public class ProjectLogic : IProjectLogic
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
                Name = value.Name,
                Priority = value.Priority,
                ProjectStatus = value.ProjectStatus,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };

            return await _repository.Create(project);
        }

        public async Task<Project> UpdateProject(int id, ProjectRequest value)
        {
            var project = await _repository.GetById(id);
            if (project == null)
            {
                throw new ArgumentException(nameof(value));
            }

            project.Name = value.Name;
            project.Priority = value.Priority;
            project.ProjectStatus = value.ProjectStatus;
            project.StartDate = value.StartDate;
            project.EndDate = value.EndDate;

            await _repository.Update(project);

            return project;
        }

        public async Task<Project> DeleteProject(int id)
        {
            var project = await _repository.GetById(id);
            if (project == null)
            {
                throw new ArgumentException(nameof(id));
            }

            await _repository.Delete(project);

            return project;
        }

        public async Task<IEnumerable<Project>> SearchProject(string? name, int? priority, ProjectStatus? projectStatus)
        {
            var projects = await _repository.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                projects = projects.Where(p => p.Name.Contains(name));
            }

            if(priority != null)
            {
                projects = projects.Where(p => p.Priority.Equals(priority));
            }

            if(projectStatus != null)
            {
                projects = projects.Where(p => p.ProjectStatus.Equals(projectStatus));
            }

            return projects;
        }
    }
}
