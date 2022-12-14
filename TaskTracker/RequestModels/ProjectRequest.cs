using System.ComponentModel.DataAnnotations;
using TaskTrackerData.Entities;
using TaskTrackerData.Entities.Statuses;

namespace TaskTracker.RequestModels
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int Priority { get; set; }
    }
}
