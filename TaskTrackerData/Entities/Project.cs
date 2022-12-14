using System.ComponentModel.DataAnnotations;
using TaskTrackerData.Entities.Statuses;
    
namespace TaskTrackerData.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int Priority { get; set; }
        public List<ProjectTask>? Tasks { get; set; }
    }
}
