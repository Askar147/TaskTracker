using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;

namespace TaskTrackerLogic
{
    public interface IProjectTaskLogic
    {
        Task<IEnumerable<ProjectTask>> GetAllProjectTasks();
        Task<ProjectTask?> GetSingleProjectTask(int id);
        Task<ProjectTask> CreateProjectTask(ProjectTaskRequest value);
        Task<ProjectTask> UpdateProjectTask(int id, ProjectTaskRequest value);
        Task<ProjectTask> DeleteProjectTask(int id);
        Task<ProjectTask> AddTaskToProject(int projectId, ProjectTaskRequest value);
        Task<ProjectTask> RemoveTaskFromProject(int id);
        Task<IEnumerable<ProjectTask>> SearchTask(string? name,string? description,ProjectTaskStatus? taskStatus, int? startPriority,int? endPriority);
    }
}
