using TaskTrackerData.Entities.Statuses;

namespace TaskTracker.RequestModels
{
    public class ProjectTaskRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTaskStatus TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}
