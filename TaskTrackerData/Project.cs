using TaskTrackerData.Statuses;

namespace TaskTrackerData
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int Priority { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
