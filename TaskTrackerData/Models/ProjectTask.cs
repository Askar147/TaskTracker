using TaskTrackerData.Models.Statuses;

namespace TaskTrackerData.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTaskStatus TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}
