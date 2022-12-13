using TaskTrackerData.Statuses;

namespace TaskTrackerData
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}
