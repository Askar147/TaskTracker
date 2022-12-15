using Microsoft.EntityFrameworkCore;
using TaskTrackerData.Entities;

namespace TaskTrackerData.Data
{
    public class TaskTrackerDataContext : DbContext
    {
        public TaskTrackerDataContext(DbContextOptions<TaskTrackerDataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>()
                .HasOne<Project>()
                .WithMany(p => p.Tasks)
                .IsRequired(false)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<ProjectTask>()
                .Property(t => t.TaskStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Project>()
                .Property(t => t.ProjectStatus)
                .HasConversion<string>();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
    }
}
