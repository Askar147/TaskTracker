using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.RequestModels;
using TaskTrackerData.Entities;

namespace TaskTrackerLogic
{
    public interface IProjectTaskLogic
    {
        Task<IEnumerable<ProjectTask>> GetAllProjectTasks();
        Task<ProjectTask> GetSingleProjectTask(int id);
        Task<ProjectTask> CreateProjectTask(ProjectTaskRequest value);
        Task<ProjectTask> UpdateProjectTask(int id, ProjectTaskRequest value);
        Task<ProjectTask> DeleteProjectTask(int id);
        Task<ProjectTask> AddTaskToProject(int projectId, ProjectTaskRequest value);
    }
}
