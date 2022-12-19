using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;
using TaskTrackerData.Repositories;

namespace TaskTrackerLogic
{
    public class ProjectTaskLogic : IProjectTaskLogic
    {
        private readonly IRepository<ProjectTask> _repository; 
        public ProjectTaskLogic(IRepository<ProjectTask> repository) 
        { 
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllProjectTasks()
        {
            return await _repository.GetAll();
        }

        public async Task<ProjectTask> GetSingleProjectTask(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ProjectTask> CreateProjectTask(ProjectTaskRequest value)
        {
            var task = new ProjectTask
            {
                Name = value.Name,
                Description = value.Description,
                Priority = value.Priority,
                TaskStatus = value.TaskStatus
            };

            return await _repository.Create(task);
        }

        public async Task<ProjectTask> UpdateProjectTask(int id, ProjectTaskRequest value)
        {
            var task = await _repository.GetById(id);

            if (task == null) 
            {
                throw new ArgumentException(nameof(value));
            }

            task.Name = value.Name;
            task.Description = value.Description;
            task.Priority = value.Priority;
            task.TaskStatus = value.TaskStatus;

            await _repository.Update(task);

            return task;
        }

        public async Task<ProjectTask> DeleteProjectTask(int id)
        {
            var task = await _repository.GetById(id);

            if (task == null)
            {
                throw new ArgumentException(nameof(id));
            }

            await _repository.Delete(task);
            return task;
        }

        public async Task<ProjectTask> AddTaskToProject(int projectId, ProjectTaskRequest value)
        {
            var task = new ProjectTask
            {
                Name = value.Name,
                Description = value.Description,
                Priority = value.Priority,
                TaskStatus = value.TaskStatus,
                ProjectId = projectId
            };

            return await _repository.Create(task);
        }

        public async Task<ProjectTask> RemoveTaskFromProject(int id)
        {
            var task = await _repository.GetById(id);

            task.ProjectId = null;

            await _repository.Update(task);

            return task;
        }

        public async Task<IEnumerable<ProjectTask>> SearchTask(string? name, string? description, ProjectTaskStatus? taskStatus, int? startPriority, int? endPriority)
        {
            var tasks = await _repository.GetAll();

            if(!string.IsNullOrEmpty(name))
            {
                tasks = tasks.Where(t => t.Name?.Contains(name) ?? false);
            }

            if (!string.IsNullOrEmpty(description))
            {
                tasks = tasks.Where(t => t.Description?.Contains(description) ?? false);
            }

            if(taskStatus != null)
            {
                tasks = tasks.Where(t => t.TaskStatus.Equals(taskStatus));
            }

            if(startPriority != null)
            {
                tasks = tasks.Where(t => t.Priority > startPriority);
            }

            if(endPriority != null)
            {
                tasks = tasks.Where(t => t.Priority < endPriority);
            }

            return tasks;
        }
    }
}
