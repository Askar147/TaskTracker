using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskTrackerData.Entities.Statuses;
    
namespace TaskTrackerData.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [DefaultValue(ProjectStatus.NotStarted)]
        public ProjectStatus ProjectStatus { get; set; }
        public int Priority { get; set; }
        public List<ProjectTask>? Tasks { get; set; }
    }
}
