using TaskTracker.RequestModels;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;

namespace TaskTrackerLogic
{
    public interface IProjectLogic
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetSingleProject(int id);
        Task<Project> CreateProject(ProjectRequest value);
        Task<Project> UpdateProject(int id, ProjectRequest value);
        Task<Project> DeleteProject(int id);
        Task<IEnumerable<Project>> SearchProject(string? name, int? priority, ProjectTaskStatus? projectStatus);
    }
}
