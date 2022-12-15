using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTrackerData.Entities.Statuses;

namespace TaskTrackerData.Entities
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public int? ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DefaultValue(ProjectTaskStatus.ToDO)]
        public ProjectTaskStatus TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}
