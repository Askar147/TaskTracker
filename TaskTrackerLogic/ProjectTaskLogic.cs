﻿using TaskTracker.RequestModels;
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
    }
}
