using TaskTracker.RequestModels;
using TaskTrackerData.Entities;

namespace TaskTrackerLogic
{
    public interface IProjectLogic
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetSingleProject(int id);
        Task<Project> CreateProject(ProjectRequest value);
        Task<Project> UpdateProject(ProjectRequest value);
        Task<Project> DeleteProject(int id);
    }
}
