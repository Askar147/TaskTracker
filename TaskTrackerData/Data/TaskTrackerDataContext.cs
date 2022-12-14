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
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<ProjectTask>()
                .HasOne<Project>()
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
    }
}
