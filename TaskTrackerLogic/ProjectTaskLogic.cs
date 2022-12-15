using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
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
                Id = value.Id,
                ProjectId = value.ProjectId,
                Name = value.Name,
                Description = value.Description,
                Priority = value.Priority,
                TaskStatus = value.TaskStatus
            };

            return await _repository.Create(task);
        }

        public async Task<ProjectTask> UpdateProjectTask(ProjectTaskRequest value)
        {
            var task = await _repository.GetById(value.Id);

            if (task == null) 
            {
                throw new ArgumentException(nameof(value));
            }

            task.ProjectId = value.ProjectId;
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
    }
}
